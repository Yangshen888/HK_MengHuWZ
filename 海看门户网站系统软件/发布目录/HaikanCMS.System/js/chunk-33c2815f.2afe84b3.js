(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-33c2815f"],{"16e0":function(t,e,a){},"3a8f":function(t,e,a){},"45e7":function(t,e,a){"use strict";a.r(e);var r=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",[a("Row",{staticStyle:{"margin-top":"10px"},attrs:{gutter:10}},t._l(t.inforCardData,function(e,r){return a("i-col",{key:"infor-"+r,staticStyle:{height:"120px","padding-bottom":"10px"},attrs:{xs:12,md:8,lg:6}},[a("infor-card",{attrs:{shadow:"",color:e.color,icon:e.icon,"icon-size":36}},[a("count-to",{attrs:{end:e.count,"count-class":"count-style"}}),a("p",[t._v(t._s(e.title))])],1)],1)}),1),t.listshow?a("Row",{staticStyle:{"margin-top":"20px"},attrs:{gutter:10}},[a("i-col",{staticStyle:{"margin-bottom":"20px"},attrs:{md:24,lg:8}},[a("Card",{attrs:{shadow:""}},[a("chart-pie",{staticStyle:{height:"300px"},attrs:{value:t.pieData,text:"新闻来源"}})],1)],1),a("i-col",{staticStyle:{"margin-bottom":"20px"},attrs:{md:24,lg:16}},[a("Card",{attrs:{shadow:""}},[a("chart-bar",{staticStyle:{height:"300px"},attrs:{value:t.barData,text:"每周访问量活跃度"}})],1)],1)],1):t._e()],1)},o=[],n=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("Card",{staticClass:"info-card-wrapper",attrs:{shadow:t.shadow,padding:0}},[a("div",{staticClass:"content-con"},[a("div",{staticClass:"left-area",style:{background:t.color,width:t.leftWidth}},[a("common-icon",{staticClass:"icon",attrs:{type:t.icon,size:t.iconSize,color:"#fff"}})],1),a("div",{staticClass:"right-area",style:{width:t.rightWidth}},[a("div",[t._t("default")],2)])])])},i=[],l=(a("c5f6"),a("cb21")),s={name:"InforCard",components:{CommonIcon:l["a"]},props:{left:{type:Number,default:36},color:{type:String,default:"#2d8cf0"},icon:{type:String,default:""},iconSize:{type:Number,default:20},shadow:{type:Boolean,default:!1}},computed:{leftWidth:function(){return"".concat(this.left,"%")},rightWidth:function(){return"".concat(100-this.left,"%")}}},c=s,d=(a("a189"),a("2877")),u=Object(d["a"])(c,n,i,!1,null,null,null),m=u.exports,f=m,h=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",{staticClass:"count-to-wrapper"},[t._t("left"),a("p",{staticClass:"content-outer"},[a("span",{class:["count-to-count-text",t.countClass],attrs:{id:t.counterId}},[t._v(t._s(t.init))]),a("i",{class:["count-to-unit-text",t.unitClass]},[t._v(t._s(t.unitText))])]),t._t("right")],2)},p=[],b=a("57f2"),y=a.n(b),g=(a("16e0"),{name:"CountTo",props:{init:{type:Number,default:0},startVal:{type:Number,default:0},end:{type:Number,required:!0},decimals:{type:Number,default:0},decimal:{type:String,default:"."},duration:{type:Number,default:2},delay:{type:Number,default:0},uneasing:{type:Boolean,default:!1},usegroup:{type:Boolean,default:!1},separator:{type:String,default:","},simplify:{type:Boolean,default:!1},unit:{type:Array,default:function(){return[[3,"K+"],[6,"M+"],[9,"B+"]]}},countClass:{type:String,default:""},unitClass:{type:String,default:""}},data:function(){return{counter:null,unitText:""}},computed:{counterId:function(){return"count_to_".concat(this._uid)}},methods:{getHandleVal:function(t,e){return{endVal:parseInt(t/Math.pow(10,this.unit[e-1][0])),unitText:this.unit[e-1][1]}},transformValue:function(t){var e=this.unit.length,a={endVal:0,unitText:""};if(t<Math.pow(10,this.unit[0][0]))a.endVal=t;else for(var r=1;r<e;r++)t>=Math.pow(10,this.unit[r-1][0])&&t<Math.pow(10,this.unit[r][0])&&(a=this.getHandleVal(t,r));return t>Math.pow(10,this.unit[e-1][0])&&(a=this.getHandleVal(t,e)),a},getValue:function(t){var e=0;if(this.simplify){var a=this.transformValue(t),r=a.endVal,o=a.unitText;this.unitText=o,e=r}else e=t;return e}},mounted:function(){var t=this;this.$nextTick(function(){var e=t.getValue(t.end);t.counter=new y.a(t.counterId,t.startVal,e,t.decimals,t.duration,{useEasing:!t.uneasing,useGrouping:t.useGroup,separator:t.separator,decimal:t.decimal}),setTimeout(function(){t.counter.error||t.counter.start()},t.delay)})},watch:{end:function(t){var e=this.getValue(t);this.counter.update(e)}}}),w=g,x=Object(d["a"])(w,h,p,!1,null,null,null),C=x.exports,S=C,v=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",{ref:"dom",staticClass:"charts chart-pie"})},V=[],A=(a("7f7f"),a("313e")),F=a.n(A),T=a("8e9a"),W=a("90de");F.a.registerTheme("tdTheme",T);var N={name:"ChartPie",props:{value:Array,text:String,subtext:String},data:function(){return{dom:null}},methods:{resize:function(){this.dom.resize()}},mounted:function(){var t=this;this.$nextTick(function(){var e=t.value.map(function(t){return t.name}),a={title:{text:t.text,subtext:t.subtext,x:"center"},tooltip:{trigger:"item",formatter:"{a} <br/>{b} : {c} ({d}%)"},legend:{orient:"vertical",left:"left",data:e},series:[{type:"pie",radius:"55%",center:["50%","60%"],data:t.value,itemStyle:{emphasis:{shadowBlur:10,shadowOffsetX:0,shadowColor:"rgba(0, 0, 0, 0.5)"}}}]};t.dom=F.a.init(t.$refs.dom,"tdTheme"),t.dom.setOption(a),Object(W["f"])(window,"resize",t.resize)})},beforeDestroy:function(){Object(W["e"])(window,"resize",this.resize)}},k=N,_=Object(d["a"])(k,v,V,!1,null,null,null),z=_.exports,O=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",{ref:"dom",staticClass:"charts chart-bar"})},D=[];a("8615"),a("ac6a"),a("456d");F.a.registerTheme("tdTheme",T);var B={name:"ChartBar",props:{value:Object,text:String,subtext:String},data:function(){return{dom:null}},methods:{resize:function(){this.dom.resize()}},mounted:function(){var t=this;this.$nextTick(function(){var e=Object.keys(t.value),a=Object.values(t.value),r={title:{text:t.text,subtext:t.subtext,x:"center"},xAxis:{type:"category",data:e},yAxis:{type:"value"},series:[{data:a,type:"bar"}]};t.dom=F.a.init(t.$refs.dom,"tdTheme"),t.dom.setOption(r),Object(W["f"])(window,"resize",t.resize)})},beforeDestroy:function(){Object(W["e"])(window,"resize",this.resize)}},E=B,j=Object(d["a"])(E,O,D,!1,null,null,null),q=j.exports,L=a("649f"),M=a("66df"),$=function(){return M["a"].request({url:"Home/Home/Number",method:"get"})},I=function(){return M["a"].request({url:"Home/Home/Chart",method:"get"})},H={name:"home",components:{InforCard:f,CountTo:S,ChartPie:z,ChartBar:q,Example:L["default"]},data:function(){return{inforCardData:[],pieData:[],barData:{},listshow:!1}},mounted:function(){this.doNumber(),this.doChart()},methods:{doNumber:function(){var t=this;$().then(function(e){console.log(e),t.inforCardData=[{title:"新闻条数",icon:"md-person-add",count:e.data.data.newNum,color:"#2d8cf0"},{title:"栏目个数",icon:"md-locate",count:e.data.data.columnNum,color:"#19be6b"},{title:"累计访问次数",icon:"md-help-circle",count:e.data.data.visitAllNum,color:"#ff9900"},{title:"今年访问次数",icon:"md-share",count:e.data.data.visitYearNum,color:"#ed3f14"}]})},doChart:function(){var t=this;I().then(function(e){if(console.log(e),t.barData={"周一":e.data.data.visitChart[0],"周二":e.data.data.visitChart[1],"周三":e.data.data.visitChart[2],"周四":e.data.data.visitChart[3],"周五":e.data.data.visitChart[4],"周六":e.data.data.visitChart[5],"周日":e.data.data.visitChart[6]},e.data.data.newChart.length>0)for(var a=0;a<e.data.data.newChart.length;a++)t.pieData.push({value:e.data.data.newChart[a].counts,name:e.data.data.newChart[a].newsTypeName});t.listshow=!0})}}},P=H,G=(a("ad89"),Object(d["a"])(P,r,o,!1,null,null,null));e["default"]=G.exports},"504c":function(t,e,a){var r=a("9e1e"),o=a("0d58"),n=a("6821"),i=a("52a7").f;t.exports=function(t){return function(e){var a,l=n(e),s=o(l),c=s.length,d=0,u=[];while(c>d)a=s[d++],r&&!i.call(l,a)||u.push(t?[a,l[a]]:l[a]);return u}}},"57f2":function(t,e,a){var r,o;!function(n,i){r=i,o="function"===typeof r?r.call(e,a,e,t):r,void 0===o||(t.exports=o)}(0,function(t,e,a){var r=function(t,e,a,r,o,n){for(var i=0,l=["webkit","moz","ms","o"],s=0;s<l.length&&!window.requestAnimationFrame;++s)window.requestAnimationFrame=window[l[s]+"RequestAnimationFrame"],window.cancelAnimationFrame=window[l[s]+"CancelAnimationFrame"]||window[l[s]+"CancelRequestAnimationFrame"];window.requestAnimationFrame||(window.requestAnimationFrame=function(t,e){var a=(new Date).getTime(),r=Math.max(0,16-(a-i)),o=window.setTimeout(function(){t(a+r)},r);return i=a+r,o}),window.cancelAnimationFrame||(window.cancelAnimationFrame=function(t){clearTimeout(t)});var c=this;for(var d in c.options={useEasing:!0,useGrouping:!0,separator:",",decimal:".",easingFn:null,formattingFn:null},n)n.hasOwnProperty(d)&&(c.options[d]=n[d]);""===c.options.separator&&(c.options.useGrouping=!1),c.options.prefix||(c.options.prefix=""),c.options.suffix||(c.options.suffix=""),c.d="string"==typeof t?document.getElementById(t):t,c.startVal=Number(e),c.endVal=Number(a),c.countDown=c.startVal>c.endVal,c.frameVal=c.startVal,c.decimals=Math.max(0,r||0),c.dec=Math.pow(10,c.decimals),c.duration=1e3*Number(o)||2e3,c.formatNumber=function(t){var e,a,r,o;if(t=t.toFixed(c.decimals),t+="",e=t.split("."),a=e[0],r=e.length>1?c.options.decimal+e[1]:"",o=/(\d+)(\d{3})/,c.options.useGrouping)for(;o.test(a);)a=a.replace(o,"$1"+c.options.separator+"$2");return c.options.prefix+a+r+c.options.suffix},c.easeOutExpo=function(t,e,a,r){return a*(1-Math.pow(2,-10*t/r))*1024/1023+e},c.easingFn=c.options.easingFn?c.options.easingFn:c.easeOutExpo,c.formattingFn=c.options.formattingFn?c.options.formattingFn:c.formatNumber,c.version=function(){return"1.7.1"},c.printValue=function(t){var e=c.formattingFn(t);"INPUT"===c.d.tagName?this.d.value=e:"text"===c.d.tagName||"tspan"===c.d.tagName?this.d.textContent=e:this.d.innerHTML=e},c.count=function(t){c.startTime||(c.startTime=t),c.timestamp=t;var e=t-c.startTime;c.remaining=c.duration-e,c.options.useEasing?c.countDown?c.frameVal=c.startVal-c.easingFn(e,0,c.startVal-c.endVal,c.duration):c.frameVal=c.easingFn(e,c.startVal,c.endVal-c.startVal,c.duration):c.countDown?c.frameVal=c.startVal-(c.startVal-c.endVal)*(e/c.duration):c.frameVal=c.startVal+(c.endVal-c.startVal)*(e/c.duration),c.countDown?c.frameVal=c.frameVal<c.endVal?c.endVal:c.frameVal:c.frameVal=c.frameVal>c.endVal?c.endVal:c.frameVal,c.frameVal=Math.round(c.frameVal*c.dec)/c.dec,c.printValue(c.frameVal),e<c.duration?c.rAF=requestAnimationFrame(c.count):c.callback&&c.callback()},c.start=function(t){return c.callback=t,c.rAF=requestAnimationFrame(c.count),!1},c.pauseResume=function(){c.paused?(c.paused=!1,delete c.startTime,c.duration=c.remaining,c.startVal=c.frameVal,requestAnimationFrame(c.count)):(c.paused=!0,cancelAnimationFrame(c.rAF))},c.reset=function(){c.paused=!1,delete c.startTime,c.startVal=e,cancelAnimationFrame(c.rAF),c.printValue(c.startVal)},c.update=function(t){cancelAnimationFrame(c.rAF),c.paused=!1,delete c.startTime,c.startVal=c.frameVal,c.endVal=Number(t),c.countDown=c.startVal>c.endVal,c.rAF=requestAnimationFrame(c.count)},c.printValue(c.startVal)};return r})},8615:function(t,e,a){var r=a("5ca1"),o=a("504c")(!1);r(r.S,"Object",{values:function(t){return o(t)}})},"8e9a":function(t){t.exports={color:["#2d8cf0","#19be6b","#ff9900","#E46CBB","#9A66E4","#ed3f14"],backgroundColor:"rgba(0,0,0,0)",textStyle:{},title:{textStyle:{color:"#516b91"},subtextStyle:{color:"#93b7e3"}},line:{itemStyle:{normal:{borderWidth:"2"}},lineStyle:{normal:{width:"2"}},symbolSize:"6",symbol:"emptyCircle",smooth:!0},radar:{itemStyle:{normal:{borderWidth:"2"}},lineStyle:{normal:{width:"2"}},symbolSize:"6",symbol:"emptyCircle",smooth:!0},bar:{itemStyle:{normal:{barBorderWidth:0,barBorderColor:"#ccc"},emphasis:{barBorderWidth:0,barBorderColor:"#ccc"}}},pie:{itemStyle:{normal:{borderWidth:0,borderColor:"#ccc"},emphasis:{borderWidth:0,borderColor:"#ccc"}}},scatter:{itemStyle:{normal:{borderWidth:0,borderColor:"#ccc"},emphasis:{borderWidth:0,borderColor:"#ccc"}}},boxplot:{itemStyle:{normal:{borderWidth:0,borderColor:"#ccc"},emphasis:{borderWidth:0,borderColor:"#ccc"}}},parallel:{itemStyle:{normal:{borderWidth:0,borderColor:"#ccc"},emphasis:{borderWidth:0,borderColor:"#ccc"}}},sankey:{itemStyle:{normal:{borderWidth:0,borderColor:"#ccc"},emphasis:{borderWidth:0,borderColor:"#ccc"}}},funnel:{itemStyle:{normal:{borderWidth:0,borderColor:"#ccc"},emphasis:{borderWidth:0,borderColor:"#ccc"}}},gauge:{itemStyle:{normal:{borderWidth:0,borderColor:"#ccc"},emphasis:{borderWidth:0,borderColor:"#ccc"}}},candlestick:{itemStyle:{normal:{color:"#edafda",color0:"transparent",borderColor:"#d680bc",borderColor0:"#8fd3e8",borderWidth:"2"}}},graph:{itemStyle:{normal:{borderWidth:0,borderColor:"#ccc"}},lineStyle:{normal:{width:1,color:"#aaa"}},symbolSize:"6",symbol:"emptyCircle",smooth:!0,color:["#2d8cf0","#19be6b","#f5ae4a","#9189d5","#56cae2","#cbb0e3"],label:{normal:{textStyle:{color:"#eee"}}}},map:{itemStyle:{normal:{areaColor:"#f3f3f3",borderColor:"#516b91",borderWidth:.5},emphasis:{areaColor:"rgba(165,231,240,1)",borderColor:"#516b91",borderWidth:1}},label:{normal:{textStyle:{color:"#000"}},emphasis:{textStyle:{color:"rgb(81,107,145)"}}}},geo:{itemStyle:{normal:{areaColor:"#f3f3f3",borderColor:"#516b91",borderWidth:.5},emphasis:{areaColor:"rgba(165,231,240,1)",borderColor:"#516b91",borderWidth:1}},label:{normal:{textStyle:{color:"#000"}},emphasis:{textStyle:{color:"rgb(81,107,145)"}}}},categoryAxis:{axisLine:{show:!0,lineStyle:{color:"#cccccc"}},axisTick:{show:!1,lineStyle:{color:"#333"}},axisLabel:{show:!0,textStyle:{color:"#999999"}},splitLine:{show:!0,lineStyle:{color:["#eeeeee"]}},splitArea:{show:!1,areaStyle:{color:["rgba(250,250,250,0.05)","rgba(200,200,200,0.02)"]}}},valueAxis:{axisLine:{show:!0,lineStyle:{color:"#cccccc"}},axisTick:{show:!1,lineStyle:{color:"#333"}},axisLabel:{show:!0,textStyle:{color:"#999999"}},splitLine:{show:!0,lineStyle:{color:["#eeeeee"]}},splitArea:{show:!1,areaStyle:{color:["rgba(250,250,250,0.05)","rgba(200,200,200,0.02)"]}}},logAxis:{axisLine:{show:!0,lineStyle:{color:"#cccccc"}},axisTick:{show:!1,lineStyle:{color:"#333"}},axisLabel:{show:!0,textStyle:{color:"#999999"}},splitLine:{show:!0,lineStyle:{color:["#eeeeee"]}},splitArea:{show:!1,areaStyle:{color:["rgba(250,250,250,0.05)","rgba(200,200,200,0.02)"]}}},timeAxis:{axisLine:{show:!0,lineStyle:{color:"#cccccc"}},axisTick:{show:!1,lineStyle:{color:"#333"}},axisLabel:{show:!0,textStyle:{color:"#999999"}},splitLine:{show:!0,lineStyle:{color:["#eeeeee"]}},splitArea:{show:!1,areaStyle:{color:["rgba(250,250,250,0.05)","rgba(200,200,200,0.02)"]}}},toolbox:{iconStyle:{normal:{borderColor:"#999"},emphasis:{borderColor:"#666"}}},legend:{textStyle:{color:"#999999"}},tooltip:{axisPointer:{lineStyle:{color:"#ccc",width:1},crossStyle:{color:"#ccc",width:1}}},timeline:{lineStyle:{color:"#8fd3e8",width:1},itemStyle:{normal:{color:"#8fd3e8",borderWidth:1},emphasis:{color:"#8fd3e8"}},controlStyle:{normal:{color:"#8fd3e8",borderColor:"#8fd3e8",borderWidth:.5},emphasis:{color:"#8fd3e8",borderColor:"#8fd3e8",borderWidth:.5}},checkpointStyle:{color:"#8fd3e8",borderColor:"rgba(138,124,168,0.37)"},label:{normal:{textStyle:{color:"#8fd3e8"}},emphasis:{textStyle:{color:"#8fd3e8"}}}},visualMap:{color:["#516b91","#59c4e6","#a5e7f0"]},dataZoom:{backgroundColor:"rgba(0,0,0,0)",dataBackgroundColor:"rgba(255,255,255,0.3)",fillerColor:"rgba(167,183,204,0.4)",handleColor:"#a7b7cc",handleSize:"100%",textStyle:{color:"#333"}},markPoint:{label:{normal:{textStyle:{color:"#eee"}},emphasis:{textStyle:{color:"#eee"}}}}}},9337:function(t,e,a){},a189:function(t,e,a){"use strict";var r=a("3a8f"),o=a.n(r);o.a},ad89:function(t,e,a){"use strict";var r=a("9337"),o=a.n(r);o.a}}]);