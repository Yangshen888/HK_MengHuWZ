(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-586fd37f"],{bc57:function(e,t,o){"use strict";var a=o("ee02"),n=o.n(a);n.a},ceb3:function(e,t,o){"use strict";o.r(t);var a=function(){var e=this,t=e.$createElement,o=e._self._c||t;return o("div",[o("Card",[o("dz-table",{attrs:{totalCount:e.stores.userinfo.query.totalCount,pageSize:e.stores.userinfo.query.pageSize},on:{"on-page-change":e.handlePageChanged,"on-page-size-change":e.handlePageSizeChanged}},[o("div",{attrs:{slot:"searcher"},slot:"searcher"},[o("section",{staticClass:"dnc-toolbar-wrap"},[o("Row",{attrs:{gutter:16}},[o("Col",{attrs:{span:"16"}},[o("Form",{attrs:{inline:""},nativeOn:{submit:function(e){e.preventDefault()}}},[o("FormItem",[o("Input",{staticStyle:{width:"150px"},attrs:{type:"text",search:"",clearable:!0,placeholder:"请输入链接"},on:{"on-search":function(t){return e.handleSearchDispatch()}},model:{value:e.stores.userinfo.query.kw,callback:function(t){e.$set(e.stores.userinfo.query,"kw",t)},expression:"stores.userinfo.query.kw"}})],1)],1)],1),o("Col",{staticClass:"dnc-toolbar-btns",attrs:{span:"8"}},[o("ButtonGroup",{staticClass:"mr3"},[o("Button",{directives:[{name:"can",rawName:"v-can",value:"delete",expression:"'delete'"}],staticClass:"txt-danger",attrs:{icon:"md-trash",title:"删除"},on:{click:function(t){return e.handleBatchCommand("delete")}}}),o("Button",{attrs:{icon:"md-refresh",title:"刷新"},on:{click:e.handleRefresh}})],1),o("Button",{directives:[{name:"can",rawName:"v-can",value:"linktype",expression:"'linktype'"}],attrs:{icon:"md-create",type:"primary",title:"链接类别"},on:{click:e.handleShowTypeWindow}},[e._v("链接类别")]),o("Button",{directives:[{name:"can",rawName:"v-can",value:"create",expression:"'create'"}],attrs:{icon:"md-create",type:"primary",title:"添加"},on:{click:e.handleShowCreateWindow}},[e._v("添加")])],1)],1)],1)]),o("Table",{ref:"tables",attrs:{slot:"table",border:!1,size:"small","highlight-row":!0,data:e.stores.userinfo.data,columns:e.stores.userinfo.columns,"row-class-name":e.rowClsRender},on:{"on-select":e.handleSelect,"on-selection-change":e.handleSelectionChange,"on-refresh":e.handleRefresh,"on-page-change":e.handlePageChanged,"on-page-size-change":e.handlePageSizeChanged},slot:"table",scopedSlots:e._u([{key:"action",fn:function(t){var a=t.row;t.index;return[o("Poptip",{attrs:{confirm:"",transfer:!0,title:"确定要删除吗?"},on:{"on-ok":function(t){return e.handleDelete(a)}}},[o("Tooltip",{attrs:{placement:"top",content:"删除",delay:1e3,transfer:!0}},[o("Button",{directives:[{name:"can",rawName:"v-can",value:"deletes",expression:"'deletes'"}],attrs:{type:"error",size:"small",shape:"circle",icon:"md-trash"}})],1)],1),o("Tooltip",{attrs:{placement:"top",content:"编辑",delay:1e3,transfer:!0}},[o("Button",{directives:[{name:"can",rawName:"v-can",value:"edit",expression:"'edit'"}],attrs:{type:"primary",size:"small",shape:"circle",icon:"md-create"},on:{click:function(t){return e.handleEdit(a)}}})],1),o("Tooltip",{attrs:{placement:"top",content:"详情",delay:1e3,transfer:!0}},[o("Button",{directives:[{name:"can",rawName:"v-can",value:"show",expression:"'show'"}],attrs:{type:"warning",size:"small",shape:"circle",icon:"md-search"},on:{click:function(t){return e.handleDetail(a)}}})],1)]}}])})],1)],1),o("Drawer",{attrs:{title:e.formTitle,width:"600","mask-closable":!1,mask:!0},model:{value:e.formModel.opened,callback:function(t){e.$set(e.formModel,"opened",t)},expression:"formModel.opened"}},[o("Form",{ref:"formdispatch",attrs:{model:e.formModel.fields,rules:e.formModel.rules,"label-position":"top"}},[o("Row",{attrs:{gutter:16}},[o("FormItem",{attrs:{label:"链接",prop:"link"}},[o("Input",{attrs:{placeholder:"链接"},model:{value:e.formModel.fields.link,callback:function(t){e.$set(e.formModel.fields,"link",t)},expression:"formModel.fields.link"}})],1)],1),o("Row",{attrs:{gutter:16}},[o("FormItem",{attrs:{"label-position":"left",label:"链接类型",prop:"type"}},[o("Select",{attrs:{filterable:""},model:{value:e.formModel.fields.linkTypeUuid,callback:function(t){e.$set(e.formModel.fields,"linkTypeUuid",t)},expression:"formModel.fields.linkTypeUuid"}},e._l(this.formModel.columlist,function(t){return o("Option",{key:t.value,attrs:{value:t.value}},[e._v(e._s(t.label))])}),1)],1)],1),o("Row",{attrs:{gutter:16}},[o("FormItem",{attrs:{label:"备注",prop:"remark"}},[o("Input",{attrs:{type:"textarea",rows:15,placeholder:"备注"},model:{value:e.formModel.fields.remark,callback:function(t){e.$set(e.formModel.fields,"remark",t)},expression:"formModel.fields.remark"}})],1)],1)],1),o("div",{staticClass:"demo-drawer-footer"},[o("Button",{attrs:{icon:"md-checkmark-circle",type:"primary"},on:{click:e.handleSubmitConsumable}},[e._v("保 存")]),o("Button",{staticStyle:{"margin-left":"30px"},attrs:{icon:"md-close"},on:{click:function(t){e.formModel.opened=!1}}},[e._v("取 消")])],1)],1),o("Drawer",{attrs:{title:"详情",width:"600","mask-closable":!1,mask:!1},model:{value:e.formModel1.opened,callback:function(t){e.$set(e.formModel1,"opened",t)},expression:"formModel1.opened"}},[o("Form",{ref:"formdispatch22",attrs:{model:e.formModel1.fields,"label-position":"top"}},[o("Row",{attrs:{gutter:16}},[o("FormItem",{attrs:{label:"链接",prop:"link"}},[o("Input",{attrs:{placeholder:"链接",readonly:!0},model:{value:e.formModel1.fields.link,callback:function(t){e.$set(e.formModel1.fields,"link",t)},expression:"formModel1.fields.link"}})],1)],1),o("Row",{attrs:{gutter:16}},[o("FormItem",{attrs:{"label-position":"left",label:"链接类型",prop:"type"}},[o("Select",{attrs:{disabled:!0,filterable:""},model:{value:e.formModel1.fields.linkTypeUuid,callback:function(t){e.$set(e.formModel1.fields,"linkTypeUuid",t)},expression:"formModel1.fields.linkTypeUuid"}},e._l(this.formModel1.columlist,function(t){return o("Option",{key:t.value,attrs:{value:t.value}},[e._v(e._s(t.label))])}),1)],1)],1),o("Row",{attrs:{gutter:16}},[o("FormItem",{attrs:{label:"备注",prop:"remark"}},[o("Input",{attrs:{placeholder:"备注",type:"textarea",rows:15,readonly:!0},model:{value:e.formModel1.fields.remark,callback:function(t){e.$set(e.formModel1.fields,"remark",t)},expression:"formModel1.fields.remark"}})],1)],1)],1),o("div",{staticClass:"demo-drawer-footer"},[o("Button",{staticStyle:{"margin-left":"30px"},attrs:{icon:"md-close"},on:{click:function(t){e.formModel1.opened=!1}}},[e._v("取 消")])],1)],1),o("Drawer",{attrs:{title:"链接类别信息",width:"600","mask-closable":!1,mask:!1},model:{value:e.formModel2.opened,callback:function(t){e.$set(e.formModel2,"opened",t)},expression:"formModel2.opened"}},[o("Form",{ref:"formdispatch3",attrs:{model:e.formModel2.fields,"label-position":"top"}},[o("Row",{attrs:{gutter:16}},[o("FormItem",{attrs:{label:"第一类别",prop:"firstType"}},[o("Input",{attrs:{placeholder:"第一类别"},model:{value:e.formModel2.fields.firstType,callback:function(t){e.$set(e.formModel2.fields,"firstType",t)},expression:"formModel2.fields.firstType"}})],1)],1),o("Row",{attrs:{gutter:16}},[o("FormItem",{attrs:{label:"第二类别",prop:"secondType"}},[o("Input",{attrs:{placeholder:"第二类别"},model:{value:e.formModel2.fields.secondType,callback:function(t){e.$set(e.formModel2.fields,"secondType",t)},expression:"formModel2.fields.secondType"}})],1)],1),o("Row",{attrs:{gutter:16}},[o("FormItem",{attrs:{label:"第三类别",prop:"thirdType"}},[o("Input",{attrs:{placeholder:"第三类别"},model:{value:e.formModel2.fields.thirdType,callback:function(t){e.$set(e.formModel2.fields,"thirdType",t)},expression:"formModel2.fields.thirdType"}})],1)],1),o("Row",{attrs:{gutter:16}},[o("FormItem",{attrs:{label:"第四类别",prop:"fourthType"}},[o("Input",{attrs:{placeholder:"第四类别"},model:{value:e.formModel2.fields.fourthType,callback:function(t){e.$set(e.formModel2.fields,"fourthType",t)},expression:"formModel2.fields.fourthType"}})],1)],1)],1),o("div",{staticClass:"demo-drawer-footer"},[o("Button",{attrs:{icon:"md-checkmark-circle",type:"primary"},on:{click:e.handleSubmitType}},[e._v("保 存")]),o("Button",{staticStyle:{"margin-left":"30px"},attrs:{icon:"md-close"},on:{click:function(t){e.formModel2.opened=!1}}},[e._v("取 消")])],1)],1)],1)},n=[],s=o("dc44"),r=o("66df"),l=function(e){return r["a"].request({url:"NewsInfo/ExternalLinkInfo/GetList",method:"post",data:e})},i=function(e){return r["a"].request({url:"NewsInfo/ExternalLinkInfo/GetCreate",method:"post",data:e})},d=function(e){return r["a"].request({url:"NewsInfo/ExternalLinkInfo/GetfoGet?guid="+e,method:"get"})},c=function(e){return r["a"].request({url:"NewsInfo/ExternalLinkInfo/GetEdit",method:"post",data:e})},u=function(e){return r["a"].request({url:"NewsInfo/ExternalLinkInfo/delete/"+e,method:"get"})},f=function(e){return r["a"].request({url:"NewsInfo/ExternalLinkInfo/batch",method:"get",params:e})},m=function(){return r["a"].request({url:"NewsInfo/ExternalLinkInfo/getType",method:"get"})},h=function(){return r["a"].request({url:"NewsInfo/ExternalLinkInfo/GetLinkType",method:"get"})},p=function(e){return r["a"].request({url:"NewsInfo/ExternalLinkInfo/SetLinkType",method:"post",data:e})},g=o("f121"),k=o("c276"),y={name:"ExternalLinkInfo",components:{DzTable:s["a"]},data:function(){return{actionurl:"",postheaders:"",loadingStatus:!1,updisabled:!1,visible:!1,commands:{delete:{name:"delete",title:"删除"},recover:{name:"recover",title:"恢复"},Import:{name:"Import",title:"导入"},export:{name:"export",title:"导出"}},inglist:[],model1:[],model2:[],stores:{userinfo:{query:{totalCount:0,pageSize:20,currentPage:1,kw:"",kw1:"",kw2:"",isDelete:0,status:-1,sort:[{direct:"DESC",field:"ID"}]},sources:{sexList:[{value:"正常营业",label:"正常营业"},{value:"暂停营业",label:"暂停营业"}]},columns:[{type:"selection",minwidth:50,key:"externalLinkUuid"},{title:"网站",key:"remark",minWidth:120,sortable:!0,ellipsis:!0,tooltip:!0},{title:"链接",key:"link",minWidth:120,sortable:!0},{title:"操作",align:"center",fixed:"right",width:100,className:"table-command-column",slot:"action"}],data:[]}},formModel:{columlist:[],opened:!1,title:"创建申请",mode:"create",dFileName:"xxxx",selection:[],fields:{link:"",remark:"",externalLinkUuid:"",linkTypeUuid:""},rules:{link:[{type:"string",required:!0,message:"请输入链接"}]}},formModel1:{columlist:[],opened:!1,selection:[],fields:{link:"",remark:"",externalLinkUuid:"",linkTypeUuid:""}},formModel2:{opened:!1,selection:[],fields:{firstType:"",secondType:"",thirdType:"",fourthType:""},rules:{}}}},computed:{formTitle:function(){return"create"===this.formModel.mode?"新增信息":"edit"===this.formModel.mode?"编辑信息":""},selectedRows:function(){return this.formModel.selection},selectedRowsId:function(){return this.formModel.selection.map(function(e){return e.externalLinkUuid})}},methods:{loadDispatchList:function(){var e=this;l(this.stores.userinfo.query).then(function(t){e.stores.userinfo.data=t.data.data,e.stores.userinfo.query.totalCount=t.data.totalCount})},handleSelect:function(e,t){},handleSelectionChange:function(e){this.formModel.selection=e},handlePageChanged:function(e){this.stores.userinfo.query.currentPage=e,this.loadDispatchList()},handlePageSizeChanged:function(e){this.stores.userinfo.query.pageSize=e,this.loadDispatchList()},rowClsRender:function(e,t){return e.isDeleted?"table-row-disabled":""},handleSearchDispatch:function(){this.loadDispatchList()},handleRefresh:function(){this.loadDispatchList()},handleResetFormDispatch:function(){this.$refs["formdispatch"].resetFields()},handleResetFormDispatch22:function(){this.$refs["formdispatch22"].resetFields()},handleResetFormDispatch3:function(){this.$refs["formdispatch3"].resetFields()},typexiala:function(){var e=this;m().then(function(t){e.formModel.columlist=t.data.data,e.formModel1.columlist=t.data.data})},handleShowTypeWindow:function(e){this.formModel2.opened=!0,this.handleResetFormDispatch3(),this.doGetLinkType()},doGetLinkType:function(){var e=this;h().then(function(t){if(200===t.data.code){console.log(t);var o=t.data.data;e.formModel2.fields=o}})},handleSubmitType:function(){var e=this;p(this.formModel2.fields).then(function(t){200===t.data.code?(e.$Message.success(t.data.message),e.formModel2.opened=!1,e.loadDispatchList()):e.$Message.warning(t.data.message)})},handleDelete:function(e){this.doDelete(e.externalLinkUuid)},doDelete:function(e){var t=this;e?u(e).then(function(e){200===e.data.code?(t.$Message.success(e.data.message),t.loadDispatchList(),t.formModel.selection=[]):t.$Message.warning(e.data.message)}):this.$Message.warning("请选择至少一条数据")},handleBatchCommand:function(e){var t=this;!this.selectedRowsId||this.selectedRowsId.length<=0?this.$Message.warning("请选择至少一条数据"):this.$Modal.confirm({title:"操作提示",content:"<p>确定要执行当前 ["+this.commands[e].title+"] 操作吗?</p>",loading:!0,onOk:function(){t.doBatchCommand(e)}})},doBatchCommand:function(e){var t=this;f({command:e,ids:this.selectedRowsId.join(",")}).then(function(e){200===e.data.code?(t.$Message.success(e.data.message),t.loadDispatchList(),t.formModel.selection=[]):t.$Message.warning(e.data.message),t.$Modal.remove()})},xz:function(e){this.stores.userinfo.query.kw=e,this.loadDispatchList()},handleShowCreateWindow:function(){this.formModel.mode="create",this.handleResetFormDispatch(),this.formModel.opened=!0},handleEdit:function(e){this.formModel.mode="edit",this.formModel.opened=!0,this.handleResetFormDispatch(),this.doLoadData(e.externalLinkUuid)},handleDetail:function(e){this.formModel1.opened=!0,this.handleResetFormDispatch22(),this.doLoadData(e.externalLinkUuid)},doLoadData:function(e){var t=this;d(e).then(function(e){if(200===e.data.code){console.log(e.data.data);var o=e.data.data;t.formModel.fields=o,t.formModel1.fields=o}})},validateUserForm:function(){var e=this,t=!1;return this.$refs["formdispatch"].validate(function(o){o?t=!0:e.$Message.error("请完善表单信息")}),t},handleSubmitConsumable:function(){var e=this.validateUserForm();e&&("create"===this.formModel.mode&&this.docreateDispatch(),"edit"===this.formModel.mode&&this.doEditDispatch())},docreateDispatch:function(){var e=this;i(this.formModel.fields).then(function(t){200===t.data.code?(e.$Message.success(t.data.message),e.formModel.opened=!1,e.loadDispatchList()):e.$Message.warning(t.data.message)})},doEditDispatch:function(){var e=this;console.log(this.formModel.fields),c(this.formModel.fields).then(function(t){200===t.data.code?(e.$Message.success(t.data.message),e.formModel.opened=!1,e.loadDispatchList()):e.$Message.warning(t.data.message)})}},mounted:function(){this.loadDispatchList(),this.typexiala()},created:function(){this.postheaders={Authorization:"Bearer "+Object(k["i"])()},this.actionurl=g["a"].baseUrl.dev+"api/v1/HqCommuna/HqCommuna/UpLoad"}},v=y,M=(o("d7f0"),o("2877")),w=Object(M["a"])(v,a,n,!1,null,null,null);t["default"]=w.exports},d130:function(e,t,o){},d7f0:function(e,t,o){"use strict";var a=o("d130"),n=o.n(a);n.a},dc44:function(e,t,o){"use strict";var a=function(){var e=this,t=e.$createElement,o=e._self._c||t;return o("div",{staticClass:"dnc-table-wrap"},[e._t("searcher",[e.searchable&&"top"===e.searchPlace?o("div",{staticClass:"search-con search-con-top"},[o("Input",{staticClass:"search-input",attrs:{clearable:"",placeholder:"输入关键字搜索"},on:{"on-change":e.handleClear},model:{value:e.searchValue,callback:function(t){e.searchValue=t},expression:"searchValue"}}),o("Button",{staticClass:"search-btn",attrs:{type:"primary"},on:{click:e.handleSearch}},[o("Icon",{attrs:{type:"search"}}),e._v("  搜索\n      ")],1)],1):e._e()]),e._t("table",[e._v("暂无数据")]),o("div",{staticStyle:{"margin-top":"15px"}},[e._t("pager",[o("Page",{attrs:{total:e.totalCount,"page-size":e.pageSize,size:"small","show-elevator":"","show-sizer":"","show-total":"","page-size-opts":e.pageSizeOpts},on:{"on-change":e.onPageChanged,"on-page-size-change":e.onPageSizeChanged}})])],2)],2)},n=[],s=(o("c5f6"),{name:"DzTable",data:function(){return{searchValue:""}},props:{searchable:{type:Boolean,default:!1},searchPlace:{type:String,default:"top"},totalCount:{type:Number,default:0},pageSize:{type:Number,default:20},showRefreshButton:{type:Boolean,default:!1},pageSizeOpts:{type:Array,default:function(){return[5,10,20,30,40,50]}}},methods:{handleClear:function(){},handleSearch:function(){this.$emit("on-search")},onPageChanged:function(e){this.$emit("on-page-change",e)},onPageSizeChanged:function(e){this.$emit("on-page-size-change",e)}}}),r=s,l=(o("bc57"),o("2877")),i=Object(l["a"])(r,a,n,!1,null,null,null);t["a"]=i.exports},ee02:function(e,t,o){}}]);