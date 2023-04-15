import axios from 'axios';

import { memoizedRefreshToken } from './refreshToken';

axios.defaults.baseURL = 'https://localhost:7169/api/v1/';

axios.interceptors.request.use(
  async (config) => {
    const accessToken = localStorage.getItem('jwtToken');

    if (accessToken) {
      config.headers = {
        ...config.headers,
        authorization: `Bearer ${accessToken}`
      };
    }

    return config;
  },
  (error) => Promise.reject(error)
);

axios.interceptors.response.use(
  (response) => response,
  async (error) => {
    const config = error?.config;

    if (error?.response?.status === 401 && !config?.sent) {
      config.sent = true;

      const result = await memoizedRefreshToken();

      if (result?.jwtToken) {
        config.headers = {
          ...config.headers,
          authorization: `Bearer ${result?.jwtToken}`
        };
      }

      return axios(config);
    }
    return Promise.reject(error);
  }
);

export const axiosPrivate = axios;
