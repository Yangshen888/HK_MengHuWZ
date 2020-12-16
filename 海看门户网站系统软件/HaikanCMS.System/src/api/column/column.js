import axios from '@/libs/api.request'

export const getColumnPList = (data) => {
  return axios.request({
    url: 'MenuTree/MenuTree/GetList',
    method: 'post',
    data
  })
}

export const getcreate = (data) => {
  return axios.request({
    url: 'MenuTree/MenuTree/Create',
    method: 'post',
    data
  })
}

//loadUser
export const loadEditData = (data) => {
  return axios.request({
    url: 'MenuTree/MenuTree/loadEditData?guid=' + data.guid,
    method: 'get'
  })
}

export const getcolumnList = (ids) => {
  return axios.request({
    url: 'MenuTree/MenuTree/getcolumnList',
    method: 'get'
  })
}

export const getedit = (data) => {
  return axios.request({
    url: 'MenuTree/MenuTree/edit',
    method: 'post',
    data
  })
}


export const deleteColumn = (ids) => {
  return axios.request({
    url: 'MenuTree/MenuTree/delete/' + ids,
    method: 'get'
  })
}

// // batch command
// export const batchCommand = (data) => {
//   return axios.request({
//     url: 'rbac/user/batch',
//     method: 'get',
//     params: data
//   })
// }

// // save user roles
// export const saveUserRoles = (data) => {
//   return axios.request({
//     url: 'rbac/user/save_roles',
//     method: 'post',
//     data
//   })
// }