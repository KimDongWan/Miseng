using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Miseng.ViewModel.UICanvas;
namespace Miseng.ViewModel
{
    public class domTreeManagementViewModel
    {

        List<UIElementInfo> _ui_Element_List;
        HtmlDocument _my_htmlDoc;
        HtmlNodeCollection _body_htmlNodeCollection;
        string _htmlPath;


        public string htmlPath
        {
            get { return _htmlPath; }
        }
        public List<UIElementInfo> UI_Element_List
        {
            get { return _ui_Element_List; }
        }

        public domTreeManagementViewModel(string htmlPath)
        {
            _ui_Element_List = new List<UIElementInfo>();
            _my_htmlDoc = new HtmlDocument();
            _my_htmlDoc.Load(htmlPath);
            _htmlPath = htmlPath;
            _body_htmlNodeCollection = _my_htmlDoc.DocumentNode.SelectNodes("//body");
            //_scriptList
        }
        public void InitList(List<UIElementInfo> uiList)
        {
            this._ui_Element_List = uiList;
        }
        #region 기존 HTML을 읽어와 elementInfoList에 저장하는 부분             초기화 부분에 사용
        public List<UIElementInfo> ReadHtmlToCS()
        {
            UIElementInfo cur_UI_Info;
            var bodyTag = _my_htmlDoc.DocumentNode.Descendants("body").ElementAt(0);
            string[] style = bodyTag.GetAttributeValue("style", "").Replace(" ", "").Replace("px", "").Replace("!important", "").Split(new Char[] { ':', ';' });
            string id = bodyTag.GetAttributeValue("id", "");
            string tooltip = bodyTag.GetAttributeValue("alt", "");
            id = "MyCanvas";
            string type = "Canvas";
            string text = "Canvas";
            cur_UI_Info = styleTransform_HTML2CS.DefaultElementAttribute(style, id, tooltip, type, text, _htmlPath);
            _ui_Element_List.Add(cur_UI_Info);

            for (int nodeIndex = 0; nodeIndex < _body_htmlNodeCollection.Descendants().Count(); nodeIndex++)
            {
                var node = _body_htmlNodeCollection.Descendants().ElementAt(nodeIndex);
                style = node.GetAttributeValue("style", "").Replace(" ", "").Replace("px", "").Split(new Char[] { ':', ';' });
                id = node.GetAttributeValue("id", "");
                tooltip = node.GetAttributeValue("alt", "");
                type = node.Name;
                text = node.InnerText;

                cur_UI_Info = styleTransform_HTML2CS.DefaultElementAttribute(style, id, tooltip, type, text, _htmlPath);
                if (node.Name == "#text") continue;

                _ui_Element_List.Add(cur_UI_Info);
            }
            return _ui_Element_List;
        }
        #endregion

        public void importScriptSrc_To_HTMLDocument(string src_path)
        {
            HtmlNode headNode = _my_htmlDoc.DocumentNode.SelectSingleNode("//html");
            bool is_target_exist = false;

            foreach (HtmlNode node in headNode.Descendants())
            {
                if (node.Name == "script" && node.GetAttributeValue("src", null).Contains(src_path))
                {
                    is_target_exist = true;

                }
            }
            if (!is_target_exist)
            {
                HtmlNode scripts = _my_htmlDoc.CreateElement("script");
                scripts.Attributes.Add("src", "js/" + src_path);
                headNode.AppendChild(scripts);
                headNode.AppendChild(_my_htmlDoc.CreateTextNode("\n"));
            }

        }

        #region Tag생성  저장시에 사용         앞으로 기존의 것과 비교해서 해당 태그만 수정할수 있도록 넣어야됨.
        public void saveHtml()
        {

            _body_htmlNodeCollection.ElementAt(0).RemoveAllChildren();
            foreach (UIElementInfo uiInfoItem in _ui_Element_List)
            {
                if (uiInfoItem.UIELEMENT_TYPE == "Canvas")
                {
                    var bodyTag = _my_htmlDoc.DocumentNode.Descendants("body").ElementAt(0);
                    bodyTag.SetAttributeValue("style", styleTransform_CS2HTML.DefaultElementAttribute(uiInfoItem));
                    bodyTag.SetAttributeValue("id", "MyCanvas");
                    bodyTag.SetAttributeValue("alt", " ");
                    _body_htmlNodeCollection.ElementAt(0).AppendChild(_my_htmlDoc.CreateTextNode("\n"));
                }
                else
                {
                    var tag = _my_htmlDoc.CreateElement(uiInfoItem.UIELEMENT_TYPE);
                    tag.SetAttributeValue("style", styleTransform_CS2HTML.DefaultElementAttribute(uiInfoItem));
                    tag.SetAttributeValue("id", uiInfoItem.UIELEMENT_ID);
                    tag.SetAttributeValue("alt", uiInfoItem.UIELEMENT_TOOLTIP);
                    if (!string.IsNullOrEmpty(uiInfoItem.UIELEMENT_TEXT))
                    {
                        tag.InnerHtml = HtmlDocument.HtmlEncode(uiInfoItem.UIELEMENT_TEXT);
                    }
                    _body_htmlNodeCollection.ElementAt(0).AppendChild(tag);
                    _body_htmlNodeCollection.ElementAt(0).AppendChild(_my_htmlDoc.CreateTextNode("\n"));
                }
            }
            _my_htmlDoc.Save(_htmlPath, Encoding.UTF8);
        }
        #endregion

    }
}