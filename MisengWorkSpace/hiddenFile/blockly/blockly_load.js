$(window).load(function(){
	$("#blocklyDiv").mousemove(function(e){
		var tempTxt = $($(".blocklySelected")[$(".blocklySelected").length - 1]).text();
		
			
		
		if(tempTxt!=null&&tempTxt != "" && m_prePageTxt != null && tempTxt != m_prePageTxt){
			m_selectPageTxt=tempTxt;
			//console.log(m_selectPageTxt);
		}
		m_prePageTxt = m_selectPageTxt;
		//if(m_selectPageTxt != null) 
			
	});

});

function SaveToDisk(fileURL, fileName) {
    // for non-IE
    if (!window.ActiveXObject) {
        var save = document.createElement('a');
        save.href = fileURL;
        save.target = '_blank';
        save.download = fileName || 'unknown';

        var event = document.createEvent('Event');
        event.initEvent('click', true, true);
        save.dispatchEvent(event);
        (window.URL || window.webkitURL).revokeObjectURL(save.href);
    }

    // for IE
    else if ( !! window.ActiveXObject && document.execCommand)     {
        var _window = window.open(fileURL, '_blank');
        _window.document.close();
        _window.document.execCommand('SaveAs', true, fileName || fileURL)
        _window.close();
    }
}