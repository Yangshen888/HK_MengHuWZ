import axios from '@/libs/api.request'

export const GetList = (data) => {
  return axios.request({
    url: 'RestsData/RestsData/list',
    method: 'post',
    data
  })
}
// GetCreate
export const GetCreate = (data) => {
  return axios.request({
    url: 'RestsData/RestsData/create',
    method: 'post',
    data
  })
}

//GetShow
export const GetShow = (data) => {
  return axios.request({
    url: 'RestsData/RestsData/show?guid=' + data,
    method: 'get'
  })
}

// GetEdit
export const GetEdit = (data) => {
  return axios.request({
    url: 'RestsData/RestsData/edit',
    method: 'post',
    data
  })
}
// delete user
export const deleteDepartment = (ids) => {
  return axios.request({
    url: 'RestsData/RestsData/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'RestsData/RestsData/batch',
    method: 'get',
    params: data
  })
}
export const deletetoFile = (data) => {
  return axios.request({
    url: 'common/common/DeleteFile',
    method: 'post',
    data
  })
}
