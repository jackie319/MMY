/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    config.language = 'zh-cn';
    //config.uiColor = '#AADC6E';
    config.toolbarGroups = [
        { name: 'clipboard', groups: ['clipboard', 'undo'] },
        { name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
        { name: 'forms', groups: ['forms'] },
        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
        { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
        { name: 'links', groups: ['links'] },
        { name: 'insert', groups: ['insert'] },
        { name: 'styles', groups: ['styles'] },
        { name: 'colors', groups: ['colors'] },
        { name: 'tools', groups: ['tools'] },
        { name: 'others', groups: ['others'] },
        { name: 'about', groups: ['about'] },
        { name: 'document', groups: ['mode', 'document', 'doctools'] }
    ];

    config.removeButtons = 'Save,NewPage,Preview,Print,Templates,Cut,Paste,Copy,PasteText,PasteFromWord,Scayt,Form,Checkbox,ImageButton,Radio,TextField,Textarea,Select,Button,HiddenField,Superscript,BulletedList,Blockquote,CreateDiv,BidiLtr,BidiRtl,Language,Unlink,Flash,Table,HorizontalRule,SpecialChar,Iframe,PageBreak,ShowBlocks,About,NumberedList,Indent,Outdent';
    config.line_height = "1;1.5;1.75;2;3;4;5";
    config.pasteFilter = null;
    //config.allowedContent = {
    //    $1: {
    //        // Use the ability to specify elements as an object.
    //        elements: CKEDITOR.dtd,
    //        attributes: "src,data-*,cke-saved-src",
    //        styles: true,
    //        classes: false
    //    }
    //};
    config.disallowedContent = "class;lang;width;height;onmousedown;href;script;*[on*]";
    //http://docs.ckeditor.com/#!/guide/dev_file_browse_upload
    config.filebrowserUploadUrl = apiConfig.image_CKEditor_upload;

    config.contentsCss = "/wwwroot/css/rich_media_content.css";
    config.bodyClass = "rich_media_content";
};
