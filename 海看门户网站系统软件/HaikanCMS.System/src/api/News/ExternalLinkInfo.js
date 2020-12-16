import axios from '@/libs/api.request'

//列表
export const GetList = (data) => {
  return axios.request({
    url: 'NewsInfo/ExternalLinkInfo/GetList',
    method: 'post',
    data
  })
}
 
//添加
export const GetCreate = (data) => {
  return axios.request({
    url: 'NewsInfo/ExternalLinkInfo/GetCreate',
    method: 'post',
    data
  })
}

//获取数据
export const GetfoGet = (data) => {
  return axios.request({
    url: 'NewsInfo/ExternalLinkInfo/GetfoGet?guid=' + data,
    method: 'get',
  })
}

//编辑
export const GetEdit = (data) => {
  return axios.request({
    url: 'NewsInfo/ExternalLinkInfo/GetEdit',
    method: 'post',
    data
  })
}

// delete department
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'NewsInfo/ExternalLinkInfo/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'NewsInfo/ExternalLinkInfo/batch',
    method: 'get',
    params: data
  })
} 

export const gettype = () => {
  return axios.request({
    url: 'NewsInfo/ExternalLinkInfo/getType',
    method: 'get'
  })
}

//获取链接类别数据
export const GetLinkType = () => {
  return axios.request({
    url: 'NewsInfo/ExternalLinkInfo/GetLinkType',
    method: 'get',
  })
}

//保存链接类别数据
export const SetLinkType = (data) => {
  return axios.request({
    url: 'NewsInfo/ExternalLinkInfo/SetLinkType',
    method: 'post',
    data
  })
}



