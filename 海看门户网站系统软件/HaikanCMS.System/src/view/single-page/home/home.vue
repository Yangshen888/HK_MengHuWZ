<template>
  <div>
    <Row :gutter="10" style="margin-top: 10px">
      <i-col
        :xs="12"
        :md="8"
        :lg="6"
        v-for="(infor, i) in inforCardData"
        :key="`infor-${i}`"
        style="height: 120px; padding-bottom: 10px"
      >
        <infor-card
          shadow
          :color="infor.color"
          :icon="infor.icon"
          :icon-size="36"
        >
          <count-to :end="infor.count" count-class="count-style" />
          <p>{{ infor.title }}</p>
        </infor-card>
      </i-col>
    </Row>
    <Row :gutter="10" style="margin-top: 20px" v-if="listshow">
      <i-col :md="24" :lg="8" style="margin-bottom: 20px">
        <Card shadow>
          <chart-pie
            style="height: 300px"
            :value="pieData"
            text="新闻来源"
          ></chart-pie>
        </Card>
      </i-col>
      <i-col :md="24" :lg="16" style="margin-bottom: 20px">
        <Card shadow>
          <chart-bar
            style="height: 300px"
            :value="barData"
            text="每周访问量活跃度"
          />
        </Card>
      </i-col>
    </Row>
    <!-- <Row>
      <Card shadow>
        <example style="height: 310px;"/>
      </Card>
    </Row> -->
  </div>
</template>

<script>
import InforCard from "_c/info-card";
import CountTo from "_c/count-to";
import { ChartPie, ChartBar } from "_c/charts";
import Example from "./example.vue";
import { Number, Chart } from "@/api/home/home";
export default {
  name: "home",
  components: {
    InforCard,
    CountTo,
    ChartPie,
    ChartBar,
    Example,
  },
  data() {
    return {
      inforCardData: [
        // { title: '新闻条数', icon: 'md-person-add', count: 150, color: '#2d8cf0' },
        // { title: '栏目个数', icon: 'md-locate', count: 2020, color: '#19be6b' },
        // { title: '累计访问次数', icon: 'md-help-circle', count: 502, color: '#ff9900' },
        // { title: '今年访问次数', icon: 'md-share', count:150, color: '#ed3f14' },
        // { title: '新增互动', icon: 'md-chatbubbles', count: 12, color: '#E46CBB' },
        // { title: '新增页面', icon: 'md-map', count: 14, color: '#9A66E4' }
      ],
      pieData: [
        // { value: 335, name: "直接访问" },
        // { value: 310, name: "邮件营销" },
        // { value: 234, name: "联盟广告" },
        // { value: 135, name: "视频广告" },
        // { value: 1548, name: "搜索引擎" },
      ],
      barData: {
        // Mon: 0,
        // Tue: 0,
        // Wed: 0,
        // Thu: 0,
        // Fri: 0,
        // Sat: 0,
        // Sun: 0
      },
      listshow:false
    };
  },
  mounted() {
    //数据统计
    this.doNumber();
    //统计图表
    this.doChart();
  },
  methods: {
    //数据统计
    doNumber() {
      Number().then((res) => {
        console.log(res);
        this.inforCardData = [
          {
            title: "新闻条数",
            icon: "md-person-add",
            count: res.data.data.newNum,
            color: "#2d8cf0",
          },
          {
            title: "栏目个数",
            icon: "md-locate",
            count: res.data.data.columnNum,
            color: "#19be6b",
          },
          {
            title: "累计访问次数",
            icon: "md-help-circle",
            count: res.data.data.visitAllNum,
            color: "#ff9900",
          },
          {
            title: "今年访问次数",
            icon: "md-share",
            count: res.data.data.visitYearNum,
            color: "#ed3f14",
          },
        ];
      });
    },
    //统计图表
    doChart(){
      Chart().then(res=>{
        console.log(res);
        this.barData={
          "周一": res.data.data.visitChart[0],
          "周二": res.data.data.visitChart[1],
          "周三": res.data.data.visitChart[2],
          "周四": res.data.data.visitChart[3],
          "周五": res.data.data.visitChart[4],
          "周六": res.data.data.visitChart[5],
          "周日": res.data.data.visitChart[6],
        };
        if(res.data.data.newChart.length>0){
          for(var i=0;i<res.data.data.newChart.length;i++){
            this.pieData.push({ value: res.data.data.newChart[i].counts, name: res.data.data.newChart[i].newsTypeName });
          }
        }
        // console.log(this.pieData);
        this.listshow=true;
      });
    }
  },
};
</script>

<style lang="less">
.count-style {
  font-size: 50px;
}
</style>
