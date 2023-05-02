import { axiosPublic } from './axiosPublic';
import mem from 'mem'

const refreshTokenFunction = async () => {
  const refreshToken = localStorage.getItem('refreshToken');
  const accessToken = localStorage.getItem('jwtToken');

  try {
    const response = await axiosPublic.post('/user/refresh-jwt-token', {
      refreshToken
    });

    const { session } = response.data;

    if (!accessToken) {
      localStorage.removeItem('jwtToken');
    }

    localStorage.setItem('jwtToken', JSON.stringify(session.jwtToken));

    return session;
  } catch (error) {
    localStorage.removeItem('jwtToken');
  }
};

const maxAge = 10000;

export const memoizedRefreshToken = mem(refreshTokenFunction, {
  maxAge
});
