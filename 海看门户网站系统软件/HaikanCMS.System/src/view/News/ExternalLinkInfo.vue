<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.userinfo.query.totalCount"
        :pageSize="stores.userinfo.query.pageSize"
        @on-page-change="handlePageChanged"
        @on-page-size-change="handlePageSizeChanged"
      >
        <div slot="searcher">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="16">
                <Form inline @submit.native.prevent>
                  <FormItem>
                    <Input
                      style="width: 150px;"
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.userinfo.query.kw"
                      placeholder="请输入链接"
                      @on-search="handleSearchDispatch()"
                    ></Input>
                  </FormItem>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button
                    v-can="'delete'"
                    class="txt-danger"
                    icon="md-trash"
                    title="删除"
                    @click="handleBatchCommand('delete')"
                  ></Button>
                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
                <Button
                  v-can="'linktype'"
                  icon="md-create"
                  type="primary"
                  @click="handleShowTypeWindow"
                  title="链接类别"
                >链接类别</Button>
                <Button
                  v-can="'create'"
                  icon="md-create"
                  type="primary"
                  @click="handleShowCreateWindow"
                  title="添加"
                >添加</Button>
              </Col>
            </Row>
          </section>
        </div>
        <Table
          slot="table"
          ref="tables"
          :border="false"
          size="small"
          :highlight-row="true"
          :data="stores.userinfo.data"
          :columns="stores.userinfo.columns"
          @on-select="handleSelect"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <!-- <template slot-scope="{row, index}" slot="auditState">
            <span>{{renderAuditState(row.auditState)}}</span>
          </template>-->
          <template slot-scope="{ row, index }" slot="action">
            <Poptip confirm :transfer="true" title="确定要删除吗?" @on-ok="handleDelete(row)">
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button type="error" v-can="'deletes'" size="small" shape="circle" icon="md-trash"></Button>
              </Tooltip>
            </Poptip>
            <Tooltip placement="top" content="编辑" :delay="1000" :transfer="true">
              <Button
                v-can="'edit'"
                type="primary"
                size="small"
                shape="circle"
                icon="md-create"
                @click="handleEdit(row)"
              ></Button>
            </Tooltip>
            <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
              <Button
                v-can="'show'"
                type="warning"
                size="small"
                shape="circle"
                icon="md-search"
                @click="handleDetail(row)"
              ></Button>
            </Tooltip>
          </template>
        </Table>
      </dz-table>
    </Card>
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
    >
      <Form
        :model="formModel.fields"
        ref="formdispatch"
        :rules="formModel.rules"
        label-position="top"
      >
        <Row :gutter="16">
          <FormItem label="链接" prop="link">
            <Input v-model="formModel.fields.link" placeholder="链接" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem
            label-position="left"
            label="链接类型"
            prop="type"
          >
            <Select v-model="formModel.fields.linkTypeUuid" filterable>
              <Option
                v-for="item in this.formModel.columlist"
                :value="item.value"
                :key="item.value"
                >{{ item.label }}</Option
              >
            </Select>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="备注" prop="remark">
            <Input v-model="formModel.fields.remark" type="textarea" :rows="15" placeholder="备注" />
          </FormItem>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitConsumable">保 存</Button>
        <Button style="margin-left: 30px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>

    <Drawer title="详情" v-model="formModel1.opened" width="600" :mask-closable="false" :mask="false">
      <Form :model="formModel1.fields" ref="formdispatch22" label-position="top">
        <Row :gutter="16">
          <FormItem label="链接" prop="link">
            <Input v-model="formModel1.fields.link" placeholder="链接" :readonly="true" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem
            label-position="left"
            label="链接类型"
            prop="type"
          >
            <Select v-model="formModel1.fields.linkTypeUuid"
                :disabled="true" filterable>
              <Option
                v-for="item in this.formModel1.columlist"
                :value="item.value"
                :key="item.value"
                >{{ item.label }}</Option
              >
            </Select>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="备注" prop="remark">
            <Input v-model="formModel1.fields.remark" placeholder="备注" type="textarea" :rows="15" :readonly="true" />
          </FormItem>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button style="margin-left: 30px" icon="md-close" @click="formModel1.opened = false">取 消</Button>
      </div>
    </Drawer>

    <Drawer title="链接类别信息" v-model="formModel2.opened" width="600" :mask-closable="false" :mask="false">
      <Form :model="formModel2.fields" ref="formdispatch3" label-position="top">
        <Row :gutter="16">
          <FormItem label="第一类别" prop="firstType">
            <Input v-model="formModel2.fields.firstType" placeholder="第一类别"  />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="第二类别" prop="secondType">
            <Input v-model="formModel2.fields.secondType" placeholder="第二类别"  />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="第三类别" prop="thirdType">
            <Input v-model="formModel2.fields.thirdType" placeholder="第三类别" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="第四类别" prop="fourthType">
            <Input v-model="formModel2.fields.fourthType" placeholder="第四类别"  />
          </FormItem>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitType">保 存</Button>
        <Button style="margin-left: 30px" icon="md-close" @click="formModel2.opened = false">取 消</Button>
      </div>
    </Drawer>
  
  </div>
</template>
<script>
import DzTable from "_c/tables/dz-table.vue";
import {
  GetList, //显示列表
  GetCreate, //新增
  GetfoGet, //获取选定信息
  GetEdit, //编辑
  batchCommand, //批量删除
  deleteDepartment, //单个删除
  SetLinkType, //保存链接类别数据
  GetLinkType, //获取链接类别数据
  gettype
} from "@/api/News/ExternalLinkInfo";
import config from "@/config";
import { getToken } from "@/libs/util";
export default {
  name: "ExternalLinkInfo",
  components: {
    DzTable
  },
  data() {
    return {
      actionurl: "",
      postheaders: "",
      loadingStatus: false,
      updisabled: false,
      visible: false,
      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        Import: { name: "Import", title: "导入" },
        export: { name: "export", title: "导出" }
      },
      inglist: [],
      model1: [],
      model2: [],
      stores: {
        userinfo: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            kw1: "",
            kw2: "",
            isDelete: 0,
            status: -1,
            sort: [
              {
                direct: "DESC",
                field: "ID"
              }
            ]
          },
          sources: {
            sexList: [
              {
                value: "正常营业",
                label: "正常营业"
              },
              {
                value: "暂停营业",
                label: "暂停营业"
              }
            ]
          },
          //列表显示
          columns: [
            { type: "selection", minwidth: 50, key: "externalLinkUuid" },
            {
              title: "网站",
              key: "remark",
              minWidth: 120,
              sortable: true,ellipsis:true,tooltip:true
            },
            {
              title: "链接",
              key: "link",
              minWidth: 120,
              sortable: true
            },
            {
              title: "操作",
              align: "center",
              fixed: "right",
              width: 100,
              className: "table-command-column",
              slot: "action"
            }
          ],
          data: []
        }
      },
      formModel: {
        columlist:[],
        opened: false,
        title: "创建申请",
        mode: "create",
        dFileName: "xxxx",
        selection: [],
        fields: {
          link: "",
          remark: "",
          externalLinkUuid: "",
          linkTypeUuid:""
        },
        rules: {
          link: [
            { type: "string", required: true, message: "请输入链接" }
          ]
        }
      },
      formModel1: {
        columlist:[],
        opened: false,
        selection: [],
        fields: {
          link: "",
          remark: "",
          externalLinkUuid: "",
          linkTypeUuid:""
        }
      },
      formModel2: {
        opened: false,
        selection: [],
        fields: {
          firstType: "",
          secondType: "",
          thirdType: "",
          fourthType: "",
        },
        rules: {
          
        }
      },
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "新增信息";
      }
      if (this.formModel.mode === "edit") {
        return "编辑信息";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map(x => x.externalLinkUuid);
    } //删除
  },
  methods: {
    //页面加载
    loadDispatchList() {
      GetList(this.stores.userinfo.query).then(res => {
        //console.log(res.data.data);
        this.stores.userinfo.data = res.data.data;
        this.stores.userinfo.query.totalCount = res.data.totalCount;
      });
    },
    handleSelect(selection, row) {},
    //多选
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    //翻页
    handlePageChanged(page) {
      this.stores.userinfo.query.currentPage = page;
      this.loadDispatchList();
    },
    //显示条数改变
    handlePageSizeChanged(pageSize) {
      this.stores.userinfo.query.pageSize = pageSize;
      this.loadDispatchList();
    },
    //行样式
    rowClsRender(row, index) {
      if (row.isDeleted) {
        return "table-row-disabled";
      }
      return "";
    },
    //搜索
    handleSearchDispatch() {
      this.loadDispatchList();
    },
    //刷新
    handleRefresh() {
      this.loadDispatchList();
    },
    //清空
    handleResetFormDispatch() {
      // this.$refs["formdispatch"].resetFields();
      this.$refs["formdispatch"].resetFields();
    },
    //清空
    handleResetFormDispatch22() {
      // this.$refs["formdispatch"].resetFields();
      this.$refs["formdispatch22"].resetFields();
    },
    //清空
    handleResetFormDispatch3() {
      this.$refs["formdispatch3"].resetFields();
    },
    typexiala() {
      gettype().then((res) => {
        this.formModel.columlist = res.data.data;
        this.formModel1.columlist = res.data.data;
      });
    },
    //链接类别
    handleShowTypeWindow(row) {
      this.formModel2.opened = true;
      this.handleResetFormDispatch3(); //清空表单
      this.doGetLinkType();
    },
    doGetLinkType(){
      GetLinkType().then(res=>{
        if (res.data.code === 200){
          console.log(res);
          var data=res.data.data;
           this.formModel2.fields=data;
          // this.formModel2.fields.firstType = data.firstType;
          // this.formModel2.fields.secondType = data.secondType;
          // this.formModel2.fields.thirdType = data.thirdType;
          // this.formModel2.fields.fourthType = data.fourthType;
          // this.formModel.columlist[0].label=data.firstType;
          // this.formModel.columlist[1].label=data.secondType;
          // this.formModel.columlist[2].label=data.thirdType;
          // this.formModel.columlist[3].label=data.fourthType;
          // this.formModel.columlist[0].value=data.firstTypeUUID;
          // this.formModel.columlist[1].value=data.secondTypeUUID;
          // this.formModel.columlist[2].value=data.thirdTypeUUID;
          // this.formModel.columlist[3].value=data.fourthTypeUUID;
        }
      });
    },
    //保存类别
    handleSubmitType(){
      SetLinkType(this.formModel2.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel2.opened = false; //关闭表单
          this.loadDispatchList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //右边删除（单个删除）
    handleDelete(row) {
      this.doDelete(row.externalLinkUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteDepartment(ids).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadDispatchList();
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //右上边删除按钮
    handleBatchCommand(command) {
      if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      this.$Modal.confirm({
        title: "操作提示",
        content:
          "<p>确定要执行当前 [" +
          this.commands[command].title +
          "] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doBatchCommand(command);
        }
      });
    },
    //右上边删除
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(",")
      }).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadDispatchList();
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },

    xz(e) {
      this.stores.userinfo.query.kw = e;
      this.loadDispatchList();
    },
    //添加按钮
    handleShowCreateWindow() {
      this.formModel.mode = "create";
      this.handleResetFormDispatch(); //清空表单
      this.formModel.opened = true;
    },
    //右边编辑
    handleEdit(row) {
      this.formModel.mode = "edit";
      this.formModel.opened = true;
      this.handleResetFormDispatch(); //清空表单
      this.doLoadData(row.externalLinkUuid);
    },
    //右边详情
    handleDetail(row) {
      this.formModel1.opened = true;
      this.handleResetFormDispatch22(); //清空表单
      this.doLoadData(row.externalLinkUuid);
    },
    //查询当前行信息
    doLoadData(id) {
      GetfoGet(id).then(res => {
        if (res.data.code === 200) {
          console.log(res.data.data);
          let data = res.data.data;
          this.formModel.fields = data;
          this.formModel1.fields = data;
        }
      });
    },
    validateUserForm() {
      let _valid = false;
      this.$refs["formdispatch"].validate(valid => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    //保存按钮
    handleSubmitConsumable() {
      let valid = this.validateUserForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.docreateDispatch();
        }
        if (this.formModel.mode === "edit") {
          this.doEditDispatch();
        }
      }
    },
    //添加（保存）
    docreateDispatch() {
      GetCreate(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.loadDispatchList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //编辑（保存）
    doEditDispatch() {
      //this.datadeal();
      console.log(this.formModel.fields);
      GetEdit(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.loadDispatchList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
  },
  mounted() {
    this.loadDispatchList(); //页面加载
    this.typexiala();
    //this.doGetLinkType();
  },
  created() {
    this.postheaders = {
      Authorization: "Bearer " + getToken()
      //Accept: "application/json, text/plain, */*"
    };
    this.actionurl = config.baseUrl.dev + "api/v1/HqCommuna/HqCommuna/UpLoad";
  }
};
</script>
<style>
.file1:hover + .fileimg1:hover {
  transform: scale(1.01, 1.01);
  box-shadow: 0 0 3px #1783ba;
}
.fileimg1 {
  width: 300px;
  height: 169px;
  float: left;
  z-index: 2;
}

.demo-upload-list {
  display: inline-block;
  width: 120px;
  height: 120px;
  text-align: center;
  line-height: 120px;
  border: 1px solid transparent;
  border-radius: 4px;
  overflow: hidden;
  background: #fff;
  position: relative;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2);
  margin-right: 4px;
}
.demo-upload-list img {
  width: 100%;
  height: 100%;
}
.demo-upload-list-cover {
  display: none;
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background: rgba(0, 0, 0, 0.6);
}
.demo-upload-list:hover .demo-upload-list-cover {
  display: block;
}
.demo-upload-list-cover i {
  color: #fff;
  font-size: 20px;
  cursor: pointer;
  margin: 0 2px;
}
</style>