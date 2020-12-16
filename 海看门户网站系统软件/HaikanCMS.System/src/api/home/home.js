import axios from '@/libs/api.request'

//数据统计
export const Number = () => {
  return axios.request({
    url: 'Home/Home/Number',
    method: 'get'
  })
}

//数据统计
export const Chart = () => {
    return axios.request({
      url: 'Home/Home/Chart',
      method: 'get'
    })
  }