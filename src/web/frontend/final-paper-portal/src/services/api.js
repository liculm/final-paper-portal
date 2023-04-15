import { axiosPrivate } from '@/common/axiosPrivate'
import { axiosPublic } from '@/common/axiosPublic'

export default {
  async getTest () {
    const response = await axiosPrivate.get('user/test');

    return response.data;
  },
  async login (data) {
    const response = await axiosPublic.post('user/login', data);

    localStorage.getItem('refreshToken')
    localStorage.setItem('refreshToken', response.data.user.refreshToken.token)
    localStorage.getItem('jwtToken')
    localStorage.setItem('jwtToken', response.data.jwtToken)
    return response.data;
  }
};
