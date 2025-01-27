import { config } from "@/config";
import { Api } from "./ClientApi";
import { useAppDispatch, useAppSelector } from "@/store/store";
import { cleanData } from "@/store/slices/meSlice";
import { login, logout } from "@/store/slices/authLoginSlice";

export const useClientApi = () => {
  const userData = useAppSelector((state: any) => state.authLogin);
  const dispatch = useAppDispatch();
  const api = new Api({
    baseURL: config.clientBaseUrl,
    headers: userData.logged
      ? { Authorization: `Bearer ${userData.accessToken}` }
      : {},
  });

  const isTokenExpired = (expiresAt?: number): boolean => {
    if (!expiresAt) return true;

    const nowSeconds = Math.floor(Date.now() / 1000);
    return nowSeconds >= expiresAt;
  };

  const refreshAccessToken = async () => {
    await api.api
      .accountReauthUpdate({ refreshToken: userData.refreshToken })
      .then((response: any) => {
        const {
          accessToken,
          refreshToken,
          expiresAccessToken,
          expiresRefreshToken,
        } = response.data;

        dispatch(
          login({
            logged: true,
            accessToken,
            refreshToken,
            expiresAccessToken,
            expiresRefreshToken,
          })
        );
      })
      .catch((error: any) => {
        console.error("Erro ao renovar o token:", error);
        dispatch(cleanData());
        dispatch(logout());
        throw error;
      });
  };

  api.instance.interceptors.request.use(
    async (config) => {
      if (isTokenExpired(userData.expiresAccessToken) && userData.logged) {
        const newAccessToken = await refreshAccessToken();
        config.headers.Authorization = `Bearer ${newAccessToken}`;
      }
      return config;
    },
    (error) => {
      return Promise.reject(error);
    }
  );

  api.instance.interceptors.response.use(
    function (response: any) {
      return response;
    },
    function (error: any) {
      let response = error.response;
      console.error(
        response.config.baseURL + response.config.url,
        error.response
      );
      if (error.response.status === 401) {
        dispatch(cleanData());
        dispatch(logout());
      }
      return Promise.reject(error?.response.data);
    }
  );

  return api;
};
