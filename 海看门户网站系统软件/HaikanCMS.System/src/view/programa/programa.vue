<template>
  <div>
    <div>
      <section class="dnc-toolbar-wrap">
        <Row :gutter="16">
          <Col span="16">
            <Form inline @submit.native.prevent>
              <FormItem style="margin: 15px 0">
                <Input
                  style="width: 300px"
                  type="text"
                  search
                  :clearable="true"
                  v-model="stores.xxx1.query.kw"
                  placeholder="输入一级栏目名称搜索..."
                  @on-search="sleet()"
                />
              </FormItem>
            </Form>
          </Col>
          <Col span="8" class="dnc-toolbar-btns" style="margin: 15px 0">
            <Button
              v-can="'create'"
              icon="md-create"
              type="primary"
              @click="handleShowCreateWindow"
              title="添加栏目"
              >添加栏目</Button
            >
          </Col>
        </Row>
      </section>
    </div>

    <vxe-table
      resizable
      :tree-config="{ children: 'children' }"
      :data="tableData"
      :checkbox-config="{ labelField: 'id', highlight: true }"
      @checkbox-change="selectChangeEvent"
    >
      <vxe-table-column
        type="checkbox"
        title="ID"
        width="160"
        tree-node
      ></vxe-table-column>
      <vxe-table-column field="columnTitle" title="栏目标题"></vxe-table-column>
      <vxe-table-column
        field="sortID"
        width="150"
        title="排序编号"
      ></vxe-table-column>
      <!-- <vxe-table-column field="cpName" title="联系人">
        <template v-slot="{ row }">
          <span v-for="(name,index) in row.cpName" :key="index">
            <Button label="small" type="text" @click="handleDeletes(row,name)">{{name}}</Button>
          </span>
        </template>
      </vxe-table-column> -->
      <vxe-table-column
        field="addTime"
        width="250"
        title="添加时间"
      ></vxe-table-column>
      <vxe-table-column title="操作" width="190">
        <template v-slot="{ row }">
          <Poptip
            confirm
            :transfer="true"
            title="确定要删除吗?"
            @on-ok="handleDelete(row)"
          >
            <Tooltip
              placement="top"
              content="删除"
              :delay="1000"
              :transfer="true"
            >
              <Button
                type="error"
                size="small"
                shape="circle"
                icon="md-trash"
                v-can="'delete'"
              ></Button>
            </Tooltip>
          </Poptip>
          <Tooltip
            placement="top"
            content="编辑"
            :delay="1000"
            :transfer="true"
          >
            <Button
              v-can="'edit'"
              type="primary"
              size="small"
              shape="circle"
              icon="md-create"
              @click="handleEdit(row)"
            ></Button>
          </Tooltip>
          <!-- <Tooltip
            placement="top"
            content="详情"
            :delay="1000"
            :transfer="true"
          >
            <Button
              v-can="'info'"
              type="success"
              size="small"
              shape="circle"
              icon="md-search"
              @click="showInfo_xxx(row)"
            ></Button>
          </Tooltip> -->
        </template>
      </vxe-table-column>
    </vxe-table>
    <vxe-pager
      perfect
      :current-page.sync="stores.xxx1.query.currentPage"
      :page-size.sync="stores.xxx1.query.pageSize"
      :total="stores.xxx1.query.totalCount"
      :layouts="[
        'PrevJump',
        'PrevPage',
        'Number',
        'NextPage',
        'NextJump',
        'Sizes',
        'FullJump',
        'Total',
      ]"
      @page-change="vtpageChage"
    ></vxe-pager>

    <!-- 添加 -->
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="800"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form
        :model="formModel.fields"
        ref="fromcreat"
        :rules="formModel.rules"
        label-position="top"
      >
        <Row :gutter="16">
          <FormItem label="栏目标题" prop="columnTitle">
            <Input v-model="formModel.fields.columnTitle" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem
            label-position="left"
            label="上级栏目(未选择默认顶级栏目)"
            prop="superiorUuid"
          >
            <Select v-model="formModel.fields.superiorUuid" filterable>
              <Option
                v-for="item in this.formModel.colum"
                :value="item.value"
                :key="item.value"
                >{{ item.label }}</Option
              >
            </Select>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label-position="left" label="栏目模型" prop="columnModel">
            <Select v-model="formModel.fields.columnModel" filterable>
              <Option
                v-for="item in this.sources.columnModelList"
                :value="item.value"
                :key="item.value"
                >{{ item.label }}</Option
              >
            </Select>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="排序编号" prop="sortId">
            <Input
              v-model="formModel.fields.sortId"
              @keyup.native="
                formModel.fields.sortId = formModel.fields.sortId.replace(
                  /[^\d]/g,
                  ''
                )
              "
              placeholder="排序编号"
            />
          </FormItem>
        </Row>
        
        <Row :gutter="16">
          <FormItem label="是否置顶" prop="isStick" label-position="left">
            <i-switch
              size="large"
              v-model="formModel.fields.isStick"
              :true-value="1"
              :false-value="0"
            >
              <span slot="open">是</span>
              <span slot="close">否</span>
            </i-switch>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="栏目内容" prop="columnContent">
            <ckeditor
              id="editor"
              :editor="editor"
              v-model="formModel.fields.columnContent"
              :config="editorConfig"
              @ready="onReady"
            ></ckeditor>
          </FormItem>
        </Row>
      </Form>

      <div class="demo-drawer-footer">
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleSubmitCreate"
          >保 存</Button
        >
        <Button
          style="margin-left: 8px"
          icon="md-close"
          @click="formModel.opened = false"
          >取 消</Button
        >
      </div>
    </Drawer>
  </div>
</template>
	<script src="https://cdn.jsdelivr.net/npm/vue"></script>
	<script src="https://cdn.jsdelivr.net/npm/xe-utils"></script>
	<script src="https://cdn.jsdelivr.net/npm/vxe-table"></script>

<script>
import config from "@/config";
import DzTable from "_c/tables/dz-table.vue";
import MyUploadAdapter from "../../global/MyUploadAdapter.js";
import { getToken } from "@/libs/util";
import DecoupledEditor from "@ckeditor/ckeditor5-build-decoupled-document";
import "@ckeditor/ckeditor5-build-decoupled-document/build/translations/zh-cn.js";
import {
  getColumnPList,
  getcolumnList, //获取全部栏目
  getcreate,
  loadEditData,
  getedit,
  deleteColumn,
} from "@/api/column/column";
export default {
  name: "programa",
  components: {
    DzTable,
  },
  data() {
    return {
      editor: DecoupledEditor,
      //editorData: "<p>Content of the editor.</p>",
      editorConfig: {
        language: "zh-cn",
      },

      tableData: [],
      commands: {
        delete: { name: "delete", title: "删除" },
      },
      panduanrowid: 0,
      rowid: "", //选择行的id
      val: "",
      usName: "",
      usGuid: "",
      clUuid: "",
      clNam: "",

      kaiguan: true,
      dio: true,
      modal1: false,
      //form保存参数

      formModel: {
        accountmanager: [],
        columlist: [],

        selection: [],
        opened: false,
        openeds: false,
        rowUuid: "",
        columnUuid: "",
        mode: "",
        superName: "",
        fields: {
          // 具体实例保存参数
          columnUuid: "",
          columnTitleText: "",
          columnTitle: "",
          columnNum: "",
          addTime: "",
          addPeople: "",
          superiorUuid: "",
          superiorMenu: "",
          columnType: "",
          columnModel: "",
          columnUrl: "",
          columnPic: "",
          staue: "",
          columnWord: "",
          columnContent: "",
          columnVideo: "",
          columnAudio: "",
          columnFile: "",
          sortId: 0,
          isStick:0
        },
        rules: {
          columnTitle: [
            { type: "string", required: true, message: "请输入栏目标题" },
          ],
          // columnContent: [
          //   { type: "string", required: true, message: "请输入内容" },
          // ],
        },
      },
      sources: {
        columnList: [],
        columnModelList: [
          {
            value: "默认栏目",
            label: "默认栏目",
          },
          {
            value: "列表栏目",
            label: "列表栏目",
          },
          {
            value: "新闻栏目",
            label: "新闻栏目",
          }
        ],
        formSource: {
          menuTree: {
            data: [],
          },
        },
      },

      //...
      //用于提交及一些保持性数据
      stores: {
        //该实例对象相关数据
        xxx1: {
          clientName: [
            {
              value: "",
              label: "",
            },
          ],
          cityList: [],

          //提交请求参数
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            isDeleted: 0,
            status: -1,
            //自定义参数
            kw: "",
            kw1: "",
            // kw2: "",
            //查询排序方式
            sort: [
              {
                direct: "DESC",
                field: "ID",
              },
            ],
          },
          //请求获得的数据集合,用于填入表格
          data: [],
          //一些自定义附件处理数据,用于下拉列表之类
          sources: {
            allStatus: [],
            allTypes: [],
            allIndustry: [],
          },
          //table的列项名称
          columns: [],
        },
        xxx2: {},
        //...
      },
      //自定义样式
      styles: {},
    };
  },
  //计算属性,以名称命名的方法体
  computed: {
    formTitle() {
      //条件
      if ((this.formModel.mode = "create")) {
        return "栏目信息";
      }
      if ((this.formModel.mode = "edit")) {
        return "栏目信息";
      }
    },
    //...
    //获取表格选择的行id
    selectRowsId() {
      //返回具体选择项的uuid
      return this.formModel.selection.map((x) => x.xxxUuid);
    },
    //...
  },

  created() {
    this.sleet();
    // getColumnPList(this.stores.xxx1.query).then((res) => {
    //   console.log("返回的数据");
    //   console.log(res);
    //   this.tableData = res.data.obj;
    //   this.stores.xxx1.query.totalCount = res.data.totalCount;
    // });
  },

  //方法集合
  methods: {
    onReady(editor) {
      // Insert the toolbar before the editable area.
      editor.ui
        .getEditableElement()
        .parentElement.insertBefore(
          editor.ui.view.toolbar.element,
          editor.ui.getEditableElement()
        );

      editor.plugins.get("FileRepository").createUploadAdapter = (loader) => {
        return new MyUploadAdapter(loader);
      };
    },
    dogetList() {
      getList(this.stores.xxx1.query).then((res) => {
        console.log("返回的数据");
        console.log(res);
        this.tableData = res.data.obj;
        this.stores.xxx1.query.totalCount = res.data.totalCount;
      });
    },
    sleet() {
      getColumnPList(this.stores.xxx1.query).then((res) => {
        console.log("返回的数据");
        console.log(res);
        this.tableData = res.data.obj;
        this.stores.xxx1.query.totalCount = res.data.totalCount;
      });
    },
    vtpageChage(e) {
      console.log(e);
      console.log(this.stores.xxx1.query);
      getList(this.stores.xxx1.query).then((res) => {
        console.log("返回的数据");
        console.log(res);
        this.tableData = res.data.obj;
        this.stores.xxx1.query.totalCount = res.data.totalCount;
        // this.lxrLis = res.data.cpName[0].split(",");
        // console.log(this.lxrLis);
      });
    },

    selectChangeEvent({ records }) {
      console.info(`勾选${records.length}个树形节点`, records);
      var cliuuid = "";
      for (let i = 0; i < records.length; i++) {
        cliuuid += records[i].columnUuid + ",";
      }
      console.info(`勾选${records.length}个树形节点uuid`, cliuuid);
    },

    btnClickEdit() {
      this.doEditDispatch();
    },
    //编辑（保存）
    doEditDispatch() {
      let reg = /^[^\s]+$/;
      if (!reg.test(this.formModel2.fields.clientName)) {
        this.$Message.warning(" 无效的名称");
        return;
      }
      console.log(this.formModel2.fields);
      editSave(this.formModel2.fields).then((res) => {
        // console.log(this.formModel.fields)
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow1();
          this.sleet();
          //this.handleRefreshMenuTreeData();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },

    //右边编辑
    handleEdit(row) {
      this.handleSwitchFormModeToEdit();
      this.handleResetFormDispatch123();
      this.handleOpenFormWindow_xxx();
      this.doLoadEditData(row.columnUuid);
    },
    handleSwitchFormModeToEdit() {
      this.formModel.mode = "edit";
    },
    doLoadEditData(columnUuid) {
      loadEditData({ guid: columnUuid }).then((res) => {
        this.formModel.fields = res.data.data;
      });
    },

    //详情
    showInfo_xxx(row) {
      this.clUuid = row.columnUuid;
      this.clNam = row.clientName;
      this.formModel6.fields.columnUuid = row.columnUuid;
      this.val = "name1";
      this.handleOpenFormWindow();
      this.handleResetFormDispatch2();
      this.doLoadEditData3(row.columnUuid);
    },
    doLoadEditData3(columnUuid) {
      loadEditData({ guid: columnUuid }).then((res) => {
        console.log(res.data);
        //this.stores.contactPerson.sources.clientName = res.data.data.clientName;
        this.formModel3.fields = res.data.data.query;
        this.formModel3.superName = res.data.data.supname;
      });
      this.doloadlxrList(columnUuid);
    },

    handleResetFormDispatch2() {
      this.$refs["formCarDispatch"].resetFields();
    },
    //查看详情打开窗口
    handleOpenFormWindow() {
      this.formModel3.opened = true;
    },

    //清空
    handleResetFormh() {
      this.$refs["fromcreat"].resetFields();
    },
    //表格行样式
    rowClsRender_xxx(row, index) {
      // if (row.isDeleted) {
      //   return "table-row-disabled";
      // }
      // return "";
    },

    handleDelete(row) {
      //console.log(row.columnUuid);
      this.doDelete(row.columnUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteColumn(ids).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.sleet();
          //this.formModel1.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },

    //刷新
    handleRefresh_xxx() {
      this.sleet();
    },
    //行选框的改变
    handleSelect(selection, row) {},
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    //排序改变(忽略?)
    handleSortChange(column) {
      this.stores.user.query.sort.direction = column.order;
      this.stores.user.query.sort.field = column.key;
      this.loadVoteinitiateList();
    },
    //搜索
    handleSearchDispatch_xxx() {
      this.sleet();
    },
    //表格翻页
    handlePageChanged_xxx(page) {
      this.stores.xxx1.query.currentPage = page;
      this.sleet();
    },
    //表格页大小改变
    handlePageSizeChanged_xxx(pageSize) {
      this.stores.xxx1.query.pageSize = pageSize;
      this.sleet();
    },

    //导入
    handleImport_xxx() {},
    //提示批量(操作)删除,command为提示操作类型
    handleBatchCommand_xxx(command) {
      if (!this.selectRowsId || this.selectRowsId.length <= 0) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      this.$Modal.confirm({
        title: "操作提示",
        content: "<p>确定要执行当前 [删除] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doBatchCommand_xxx(command);
        },
      });
    },
    //批量(操作)删除,command为提示操作类型
    doBatchCommand_xxx(command) {
      batchCommand_xxx({
        command: command,
        ids: this.selectRowsId.join(","),
      }).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.sleet();
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },

    //编辑时打开窗口
    handleOpenFormWindow_xxx() {
      this.formModel.opened = true;
    },
    //启用编辑的状态
    handleSwitchFormModeToEdit_xxx() {
      this.formModel.mode = "edit";
    },
    //启用添加的状态
    handleSwitchFormModeToCreate_xxx() {
      this.formModel.mode = "create";
    },
    //编辑按钮
    handleEdit_xxx(row) {
      this.handleSwitchFormModeToEdit_xxx();
      this.handleOpenFormWindow_xxx();
      //查询该条数据内容
    },
    //添加按钮
    handleShowCreateWindow() {
      this.handleSwitchFormModeToCreate_xxx();
      this.handleResetFormDispatch123();
      this.handleOpenFormWindow_xxx();
    },

    handleResetFormDispatch123() {
      this.$refs["fromcreat"].resetFields();
      this.formModel.fields.superiorUuid = "";
      this.formModel.fields.columnTitle = "";
      this.formModel.fields.columnModel = "";
      this.formModel.fields.columnContent = "";
      this.formModel.fields.sortId = 0;
    },
    //表单验证
    validateForm_xxx() {
      let _valid = false;
      this.$refs["fromcreat"].validate((valid) => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    //保存按钮

    _xxx() {
      let valid = this.validateForm_xxx();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.doCreate_xxx();
        }
        if (this.formModel.mode === "edit") {
          this.doEdit_xxx();
        }
      }
    },

    handleSubmitCreate() {
      let valid = this.validateForm_xxx();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.doCreate_xxx();
        }
        if (this.formModel.mode === "edit") {
          this.doEdit_xxx();
        }
      }
    },
    //保存添加数据
    doCreate_xxx() {
      getcreate(this.formModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleResetFormDispatch123();
          this.formModel.opened = false;
          this.tableData = [];
          console.log(333);
          this.sleet();
          //this.handleRefreshMenuTreeData();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //保存编辑数据
    doEdit_xxx() {
      getedit(this.formModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false;
          this.sleet();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //保存-分配经理权限
    editmangerpower() {
      save_manger(this.formModefenpei).then((res) => {
        this.formModefenpei.opened = false;
        this.$Message.warning("成功");
        this.sleet();
      });
    },

    //栏目经理选择框内的选择项
    naaccountmanager(row) {
      this.formModefenpei.rowUuid = row.clientManager;
      this.formModefenpei.columnUuid = row.columnUuid;
      this.formModefenpei.opened = true;
      geallmanager().then((res) => {
        this.formModefenpei.accountmanager = res.data.data;
      });
    },
    //取消保存
    doCancel_xxx() {
      this.formModel3.opened = false;
    },
    doLoadMenuTree(selectedGuid) {
      loadMenuTree().then((res) => {
        this.sources.formSource.menuTree.data = res.data.data;
      });
    },
    handleMenuTreeSelectChange(nodes) {
      var node = nodes[0];
      if (node) {
        this.formModel2.fields.superiorUuid = node.guid;
        this.formModel2.superName = node.title;
      }
    },
    handleMenuTreeSelectChange1(nodes) {
      var node = nodes[0];
      if (node) {
        this.formModel.fields.superiorUuid = node.guid;
        this.formModel.superName = node.title;
      }
    },

    handleRefreshMenuTreeData(selectedGuid) {
      this.doLoadMenuTree(selectedGuid || null);
    },
    doLoadMenuTree2(selectedGuid) {
      loadMenuTree().then((res) => {
        this.sources.formSource1.menuTree.data = res.data.data;
      });
    },
    supname(row) {
      this.handleRefreshMenuTreeData2(row.columnUuid);
    },
    handleRefreshMenuTreeData2(selectedGuid) {
      this.doLoadMenuTree2(selectedGuid || null);
    },
    handleMenuTreeSelectChange2(nodes) {
      var node = nodes[0];
      if (node) {
        this.formModel.fields.superiorUuid = node.guid;
        this.formModel.superName = node.title;
      }
    },
    loadcolumnList() {
      getcolumnList().then((res) => {
        console.log(9988);
        console.log(res);
        this.formModel.colum = res.data.data;
      });
    },
    doloadlxrList(guid) {
      loadSimpleList({ guid: guid }).then((res) => {
        // //console.log(res.data.data);
        this.xglxrlist = res.data.data;
      });
    },
    testlxr(data) {},
    //---------------第二个模型-------------------

    //----------------slot-----------------
    Is_xxx(type) {
      let rType = "未知";

      return rType;
    },
    CheckState(state) {
      if (state == 0) {
        return "否";
      }
      if (state == 1) {
        return "是";
      }
    },
    //------------------杂项---------------------
    //将日期转为YY-MM-DD hh:mm:ss字符串格式
    dateToString(date) {
      let year = date.getFullYear();
      let month = date.getMonth() + 1;
      let day = date.getDate();
      let hour = date.getHours();
      let minute = date.getMinutes();
      let second = date.getSeconds();
      return (
        year +
        "-" +
        month +
        "-" +
        day +
        " " +
        hour +
        ":" +
        minute +
        ":" +
        second
      );
    },
  },
  //页面加载时执行
  mounted() {
    this.loadcolumnList();
    this.sleet();
  },
};
</script>
<style>
@import url("https://cdn.jsdelivr.net/npm/vxe-table/lib/index.css");
</style>
<style >
.ck-editor__editable {
  min-height: 500px;
}
</style>