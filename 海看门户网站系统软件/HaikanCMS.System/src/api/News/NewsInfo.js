import axios from '@/libs/api.request'

export const getScenicspotList = (data) => {
  return axios.request({
    url: 'NewsInfo/NewsInfo/list',
    method: 'post',
    data
  })
}
// createScenicspot
export const createScenicspot = (data) => {
  return axios.request({
    url: 'NewsInfo/NewsInfo/create',
    method: 'post',
    data
  })
}

//loadScenicspot
export const loadScenicspot = (data) => {
  return axios.request({
    url: 'NewsInfo/NewsInfo/edit/' + data.guid,
    method: 'get'
  })
}

// editScenicspot
export const editScenicspot = (data) => {
  return axios.request({
    url: 'NewsInfo/NewsInfo/edit',
    method: 'post',
    data
  })
}
// delete user
export const deleteScenicspot = (ids) => {
  return axios.request({
    url: 'NewsInfo/NewsInfo/delete/' + ids,
    method: 'get'
  })
}

export const gettype = (ids) => {
  return axios.request({
    url: 'NewsInfo/NewsInfo/getNewsType',
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'NewsInfo/NewsInfo/batch',
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
