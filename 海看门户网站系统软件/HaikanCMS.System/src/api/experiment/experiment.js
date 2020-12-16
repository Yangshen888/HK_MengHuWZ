import axios from '@/libs/api.request'

export const getScenicspotList = (data) => {
  return axios.request({
    url: 'Experiment/Experiment/list',
    method: 'post',
    data
  })
}
// createScenicspot
export const createScenicspot = (data) => {
  return axios.request({
    url: 'Experiment/Experiment/create',
    method: 'post',
    data
  })
}

//loadScenicspot
export const loadScenicspot = (data) => {
  return axios.request({
    url: 'Experiment/Experiment/edit/' + data.guid,
    method: 'get'
  })
}

// editScenicspot
export const editScenicspot = (data) => {
  return axios.request({
    url: 'Experiment/Experiment/edit',
    method: 'post',
    data
  })
}
// delete user
export const deleteScenicspot = (ids) => {
  return axios.request({
    url: 'Experiment/Experiment/delete/' + ids,
    method: 'get'
  })
}

export const gettype = (ids) => {
    return axios.request({
      url: 'Experiment/Experiment/getNewsType',
      method: 'get'
    })
  }

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'Experiment/Experiment/batch',
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
