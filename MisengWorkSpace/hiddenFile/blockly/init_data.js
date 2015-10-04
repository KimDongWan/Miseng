var targetName = "";
var targetType = "";
var startXML = "";
var currentScene = "";
var sceneList = new Array();
var scenepair = new Array();
var m_selectPageTxt ="";
var m_prePageTxt = "";
var toolbox;
function saveBlock() {
	  
    var xml = Blockly.Xml.workspaceToDom(Blockly.getMainWorkspace());
    // Gets the current URL, not including the hash.
    //var url = window.location.href.split('#')[0];
    //window.localStorage.setItem(url, Blockly.Xml.domToText(xml));
    xml.setAttribute("id","startBlocks");
    xml.setAttribute("style","display:none");
    return Blockly.Xml.domToText(xml);
};
/*
function setStartXML(targetName,targetType,currentScene,startXML){
	//var startXML = '<xml id="startBlocks" style="display: none"><block type="controls_if" id="1" inline="false" x="20" y="20"><mutation else="1"></mutation><value name="IF0"><block type="logic_compare" id="2" inline="true"><field name="OP">EQ</field><value name="A"><block type="math_arithmetic" id="3" inline="true"><field name="OP">MULTIPLY</field><value name="A"><block type="math_number" id="4"><field name="NUM">6</field></block></value><value name="B"><block type="math_number" id="5"><field name="NUM">7</field></block></value></block></value><value name="B"><block type="math_number" id="6"><field name="NUM">42</field></block></value></block></value><statement name="DO0"><block type="text_print" id="7" inline="false"><value name="TEXT"><block type="text" id="8"><field name="TEXT">Dont panic</field></block></value></block></statement><statement name="ELSE"><block type="text_print" id="9" inline="false"><value name="TEXT"><block type="text" id="10"><field name="TEXT">Panic</field></block></value></block></statement></block></xml>';
	localStorage.setItem("startData",startXML);
	localStorage.setItem("targetName",targetName);
	localStorage.setItem("targetType",targetType);
	localStorage.setItem("currentScene",currentScene);
	location.reload();
	
}
function getStartXML(){	
	
	targetName = localStorage.getItem('targetName');
	targetType = localStorage.getItem('targetType');
	startXML   = localStorage.getItem('startData');	
	currentScene = localStorage.getItem("currentScene");
	
	localStorage.removeItem("startData");
	localStorage.removeItem("targetName");
	localStorage.removeItem("taretType");
	localStorage.removeItem("currentScene");
}*/
function setToolBox(){

		
	toolbox =  '<xml id="toolbox" style="display: none">';
	toolbox += '<category name="Common">';
	toolbox += '<category name="general">';
	toolbox += '<block type="start_accel_Sensor"></block>';
	toolbox += '<block type="transmit_geartounity"></block>';
	toolbox += '<block type="transform_scene"></block>';
	toolbox += '<block type="rotate_angle"></block>';
	toolbox += '<block	type="move_target"></block>';
	toolbox += '</category>';
	toolbox += '<category name="Logic">';
	toolbox += '<block type="observe"></block> <block type="controls_if"></block>';
	toolbox += '<block type="logic_compare"></block> <block type="logic_operation"></block>';
	toolbox += '<block type="logic_negate"></block> <block type="logic_boolean"></block>';
	toolbox += '</category>';
	toolbox += '<category name="Loops">';
	toolbox += '<block type="controls_repeat_ext">';
	toolbox += '<value name="TIMES">';
	toolbox += '<block type="math_number">';
	toolbox += '<field name="NUM">10</field>';
	toolbox += '</block>';
	toolbox += '</value>';
	toolbox += '</block>';
	toolbox += '<block type="controls_whileUntil"></block>';
	toolbox += '</category>';
	toolbox += '<category name="Text">';
	toolbox += '<block type="text_print"></block>';
	toolbox += '<block type="text"></block>';
	toolbox += '<block type="text_length"></block>';
	toolbox += '</category>';
	toolbox += '<category name="Event">';
	toolbox += '<block type="touch_event"></block>';
	toolbox += '<block type="onload_event"></block>';
	toolbox += '<block type="mouse_point"></block>';
	toolbox += '<block type="vibration"></block> ';
	toolbox += '</category>';
	toolbox += '</category>';
	toolbox += '<category name="Behavior">';
	toolbox += '<block type="import_behavior_function"></block>';
	toolbox += '<block type="is_up_motion"></block>';
	toolbox += '<block type="is_down_motion"></block>';
	toolbox += '<block type="is_right_motion"></block>';
	toolbox += '<block type="is_left_motion"></block>';
	toolbox += '<block type="is_ratate_clockwise"></block>';
	toolbox += '<block type="is_ratate_counterclockwise"></block>';
	toolbox += '</category>';
	toolbox += '<category name="SnowBoard">';
	toolbox += '<block type="import_snowboard_function"></block>';
	toolbox += '<block type="is_boardjump"></block>';
	toolbox += '<block type="is_snowboard_turn_left"></block>';
	toolbox += '<block type="is_snowboard_turn_right"></block>';
	toolbox += '<block type="is_snowboard_break"></block>';
	toolbox += '<block type="is_snowboard_accel"></block>';
	toolbox += '</category>';
	toolbox += '<category name="Golf">';
	toolbox += '<block type="import_golf_function"></block>';
	toolbox += '<block type="is_golfshot"></block>';
	toolbox += '</category>';
	toolbox += '</xml>';
	
	Blockly.inject(document.getElementById('blocklyDiv'),
		      {toolbox: toolbox});
}
function setStartXML(_targetName,_targetType,_currentScene,startXML){
	Blockly.mainWorkspace.clear();
	
	
	targetName = _targetName;
	targetType = _targetType;
	currentScene = _currentScene;
	//setToolBox();
	init_custumBlock();
	if(startXML != null && startXML != "")
		BlocklyStorage.restoreBlocks(startXML);
	//$('body').html('<div id="blocklyDiv"></div>\n'+toolbox);
	$('body').change();
}
function getJSCode() {
	// Generate JavaScript code and display it.
	Blockly.JavaScript.INFINITE_LOOP_TRAP = null;
	var code = Blockly.JavaScript.workspaceToCode();
	return code;
}
// getStartXML();


$('body').ready(function(){
	if(startXML!=null)
		$('body').html($('body').html()+startXML);
});

$(window).load(function(){
	Blockly.inject(document.getElementById('blocklyDiv'), {
		toolbox : document.getElementById('toolbox')
	});
	if(document.getElementById('startBlocks'))
			Blockly.Xml.domToWorkspace(Blockly.mainWorkspace, document.getElementById('startBlocks'));	
});


/*
 * function getSelectBlockName(){ if(m_selectPageTxt == null || m_selectPageTxt ==
 * "") return " "; return m_selectPageTxt; }
 */
function getSelectBlockName(){
	switch(m_selectPageTxt){
	case 'is SnowBoard Jump':
	case 'Turn Left' :
	case 'Turn Right':
	case 'Break':
	case 'Accel':
	case 'Is Shot':
	case 'Raised up a gear':
	case 'fell down gear':
	case 'Gear Move to Right':
	case 'Gear Move to Left':
	case 'Gear Rotate Clockwise':
	case 'Gear Rotate CounterClockWise':
		return m_selectPageTxt;
	default  : 
		return " ";
	}		
}


scenepair.push("test1");
scenepair.push("test1");
sceneList.push(scenepair);
scenepair = new Array();
scenepair.push("test2");
scenepair.push("test2");
sceneList.push(scenepair);



