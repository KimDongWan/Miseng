function init_custumBlock(){
Blockly.Blocks['transform_scene'] = {
  init: function() {
    this.setHelpUrl('http://www.example.com/');
    this.appendDummyInput()
        .appendField("Change the screen as ")
        .appendField(new Blockly.FieldTextInput("SceneName"), "SceneName");        
    this.setInputsInline(true);
    this.setPreviousStatement(true, "null");
    this.setNextStatement(true, "null");
    this.setTooltip('');
  }
};
Blockly.JavaScript['transform_scene'] = function(block) {
     var text_scenename = block.getFieldValue('SceneName');
     // TODO: Assemble JavaScript into code variable.
     var code = 'window.location.replace("'+text_scenename+'.html'+'");\n';
     return code;
   };

Blockly.Blocks['is_boardjump'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("is SnowBoard Jump");
        this.setInputsInline(true);
        this.setOutput(true, "Boolean");
        this.setTooltip('');
    }
};
	Blockly.Blocks['recieve_message'] = {
		init : function() {
			this.setHelpUrl('http://www.example.com/');
			this.appendValueInput("condition").setCheck("String").appendField(
					"If Recieve from Unity Message ").appendField(
					new Blockly.FieldDropdown([ [ "==", "equ" ],
							[ "!=", "nequ" ]]), "equ");
			this.appendStatementInput("stateMent").setCheck("null");
			this.setInputsInline(true);
			this.setPreviousStatement(true, "null");
			this.setNextStatement(true, "null");
			this.setTooltip('');
		}
	};
	Blockly.JavaScript['recieve_message'] = function(block) {
		var value_condition = Blockly.JavaScript.valueToCode(block,
				'condition', Blockly.JavaScript.ORDER_ATOMIC);
		var dropdown_equ = block.getFieldValue('equ');
		var condition;
		if(dropdown_equ=='equ')
			condition = "==";
		else
			condition = "!=";
		var statements_statement = Blockly.JavaScript.statementToCode(block,
				'stateMent');
		// TODO: Assemble JavaScript into code variable.
		var code = '$(document).ready(function(e){\n';
		code+='\t$("body").on("newMessageFromUnity",function(event){\n';
		code+='\t\tif('+"RecieveData"+condition+value_condition+'){\n\t\t';
		code+= statements_statement;
		code+= '}\n';
		code+= '});});';
		return code;
	};


Blockly.JavaScript['is_boardjump'] = function (block) {    
    var code;
    code = 'is_boardjump()';

    return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['transmit_geartounity'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Send a").appendField(
                new Blockly.FieldTextInput("Message"), "message")
                .appendField(" signal to unity3D");
        this.setInputsInline(true);
        this.setPreviousStatement(true);
        this.setNextStatement(true);
        this.setTooltip('');
    }
};
Blockly.JavaScript['transmit_geartounity'] = function (block) {
    var text_message = block.getFieldValue('message');
    // TODO: Assemble JavaScript into code variable.
    var code = 'sendData("'+text_message + '");\n';
    return code;
};


Blockly.Blocks['rotate_angle'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Rotating the "+targetName+ "_" + targetType + " to ").appendField(
                new Blockly.FieldAngle("90"), "degree");
        this.setPreviousStatement(true);
        this.setNextStatement(true);
        this.setTooltip('');
    }
};

Blockly.JavaScript['rotate_angle'] = function (block) {
    var angle_name = block.getFieldValue('degree');
    // TODO: Assemble JavaScript into code variable.
    var code = 'document.getElementById("'+targetName+'").style.transform = "rotate('+angle_name+'deg)";\n';
    return code;
};
Blockly.Blocks['move_target'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendValueInput("X").setCheck("Number").appendField(
                targetName + "_" + targetType + " move to (");
        this.appendValueInput("Y").setCheck("Number").appendField(") , (");
        this.appendDummyInput().appendField(")");
        this.setInputsInline(true);
        this.setPreviousStatement(true);
        this.setNextStatement(true);
        this.setTooltip('');
    }
};
Blockly.JavaScript['move_target'] = function (block) {
    var value_x = Blockly.JavaScript.valueToCode(block, 'X',
            Blockly.JavaScript.ORDER_ATOMIC);
    var value_y = Blockly.JavaScript.valueToCode(block, 'Y',
            Blockly.JavaScript.ORDER_ATOMIC);
    // TODO: Assemble JavaScript into code variable.
    var code = 'document.getElementById("'+targetName +'").style.top ='+ value_x+';\ndoucment.getElementById("'+targetName+'").style.left =' + value_y+';\n';
    return code;
};

Blockly.Blocks['observe'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.setColour(210);
        this.appendDummyInput().appendField("Observed to ");
        this.appendStatementInput("callBack").setCheck("null");
        this.setPreviousStatement(true);
        this.setNextStatement(true);
        this.setTooltip('');
    }
};
Blockly.JavaScript['observe'] = function (block) {
    var statements_callback = Blockly.JavaScript.statementToCode(block, 'callBack');
    // TODO: Assemble JavaScript into code variable.
    var code = ' window.addEventListener("devicemotion", function(e) {\n\tax = e.accelerationIncludingGravity.x;\n\tay = -e.accelerationIncludingGravity.y;\n\taz = -e.accelerationIncludingGravity.z;\n\t';
    code+='list_ax.push(ax);\n\tlist_ay.push(ay);\n\tlist_az.push(az);\n\t';
    code+='temp_ax = ax;\n\ttemp_ay = ay;\n\ttemp_az = az;\n\t';
    code+='if(list_ax.length>50){ list_ax = new Array(); list_ay= new Array(); list_az= new Array();}\n\t';
    code += statements_callback;
    code += '\n});\n';
    return code;
};

Blockly.Blocks['touch_event'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("If touch the "+targetName + "_" + targetType);
        this.appendStatementInput("callBack").setCheck("null").appendField(
                "do");
        this.setPreviousStatement(true);
        this.setNextStatement(true);
        this.setTooltip('');
    }
};

Blockly.JavaScript['touch_event'] = function (block) {
    var statements_callback = Blockly.JavaScript.statementToCode(block,
            'callBack');
    // TODO: Assemble JavaScript into code variable.
    var code = 'document.getElementById("'+targetName+'").addEventListener("click",function(e){\n';
    code+= statements_callback;
    code+='});\n';
    return code;
};

Blockly.Blocks['onload_event'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("If the "+currentScene).appendField(
                "is Loaded");
        this.appendStatementInput("callBack").setCheck("null").appendField(
                "do");
        this.setPreviousStatement(true);
        this.setNextStatement(true);
        this.setTooltip('');
    }
};

Blockly.JavaScript['onload_event'] = function (block) {
    var statements_callback = Blockly.JavaScript.statementToCode(block,
            'callBack');
    // TODO: Assemble JavaScript into code variable.
    var code = 'window.onload = function(e){';
    code += statements_callback;
    code +='\n};\n';
    return code;
};

Blockly.Blocks['mouse_point'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Mouse Position( ").appendField(
                new Blockly.FieldDropdown([["X", "X"], ["Y", "Y"]]),
                "MousePosition").appendField(" )");
        this.setInputsInline(true);
        this.setOutput(true, "Number");
        this.setTooltip('');
    }
};

Blockly.JavaScript['mouse_point'] = function (block) {
    var dropdown_mouseposition = block.getFieldValue('MousePosition');
    // TODO: Assemble JavaScript into code variable.
    var code = '';
    if(dropdown_mouseposition == 'X') code = 'mouse_X\n';
    else   code = 'mouse_Y\n';
    // TODO: Change ORDER_NONE to the correct strength.
    return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['vibration'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField(
        "Vibration for ").appendField(new Blockly.FieldTextInput("3"), "second").appendField(
                "seconds");
        this.setPreviousStatement(true, "null");
        this.setNextStatement(true, "null");
        this.setTooltip('');
    }
};

Blockly.JavaScript['vibration'] = function (block) {
    var text_second = block.getFieldValue('second');
    // TODO: Assemble JavaScript into code variable.
    var code = 'navigator.vibrate(' + text_second*1000 + ');\n';
    return code;
};

Blockly.Blocks['is_up_motion'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Raised up a gear");
        this.setOutput(true, "Boolean");
        this.setTooltip('');
    }
};

Blockly.JavaScript['is_up_motion'] = function (block) {
    // TODO: Assemble JavaScript into code variable.

    var code = 'is_up_motion()\n';
    // TODO: Change ORDER_NONE to the correct strength.
    return [code, Blockly.JavaScript.ORDER_NONE];
};
Blockly.Blocks['is_down_motion'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("fell down gear");
        this.setOutput(true, "Boolean");
        this.setTooltip('');
    }
};

Blockly.JavaScript['is_down_motion'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'is_down_motion()\n';
    // TODO: Change ORDER_NONE to the correct strength.
    return [code, Blockly.JavaScript.ORDER_NONE];
};
Blockly.Blocks['is_right_motion'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Gear Move to Right");
        this.setOutput(true, "Boolean");
        this.setTooltip('');
    }
};
Blockly.JavaScript['is_right_motion'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'is_right_motion()\n';
    // TODO: Change ORDER_NONE to the correct strength.
    return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['is_left_motion'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Gear Move to Left");
        this.setOutput(true, "Boolean");
        this.setTooltip('');
    }
};

Blockly.JavaScript['is_left_motion'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'is_left_motion()\n';
    // TODO: Change ORDER_NONE to the correct strength.
    return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['is_rotate_clockwise'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Gear Rotate Clockwise");
        this.setOutput(true, "Boolean");
        this.setTooltip('');
    }
};
Blockly.JavaScript['is_rotate_clockwise'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'is_rotate_clockwise()\n';
    // TODO: Change ORDER_NONE to the correct strength.
    return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['is_rotate_counterclockwise'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Gear Rotate CounterClockWise");
        this.setOutput(true, "Boolean");
        this.setTooltip('');
    }
};
Blockly.JavaScript['is_rotate_counterclockwise'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'is_rotate_counterclockwise()\n';
    // TODO: Change ORDER_NONE to the correct strength.
    return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['is_snowboard_turn_left'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Turn Left");
        this.setOutput(true, "Boolean");
        this.setTooltip('');
    }
};

Blockly.JavaScript['is_snowboard_turn_left'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'is_snowboard_turn_left()\n';
    // TODO: Change ORDER_NONE to the correct strength.
    return [code, Blockly.JavaScript.ORDER_NONE];
};
Blockly.Blocks['is_snowboard_turn_right'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Turn Right");
        this.setOutput(true, "Boolean");
        this.setTooltip('');
    }
};
Blockly.JavaScript['is_snowboard_turn_right'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'is_snowboard_turn_right()\n';
    // TODO: Change ORDER_NONE to the correct strength.
    return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['is_snowboard_break'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Break");
        this.setOutput(true, "Boolean");
        this.setTooltip('');
    }
};
Blockly.JavaScript['is_snowboard_break'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'is_snowboard_break()\n';
    // TODO: Change ORDER_NONE to the correct strength.
    return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['is_snowboard_accel'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Accel");
        this.setOutput(true, "Boolean");
        this.setTooltip('');
    }
};
Blockly.JavaScript['is_snowboard_accel'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'is_snowboard_accel()\n';
    // TODO: Change ORDER_NONE to the correct strength.
    return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['is_golfshot'] = {
    init: function () {
        this.setHelpUrl('http://www.example.com/');
        this.appendDummyInput().appendField("Is Shot");
        this.setOutput(true, "Boolean");
        this.setTooltip('');
    }
};
Blockly.JavaScript['is_golfshot'] = function (block) {
    // TODO: Assemble JavaScript into code variable.
    var code = 'is_golfshot()\n';
    // TODO: Change ORDER_NONE to the correct strength.
    return [code, Blockly.JavaScript.ORDER_NONE];
};
};
