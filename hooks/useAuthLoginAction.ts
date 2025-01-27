import { useAppDispatch } from "@/store/store";
import { login, logout } from "../store/slices/authLoginSlice";
import { useClientApi } from "@/apis/client";
import { CreateUserModel } from "@/apis/client/ClientApi";
import { cleanData } from "@/store/slices/meSlice";
export function useAuthLoginAction() {
  const dispatch = useAppDispatch();
  const clientApi = useClientApi();

  return {
    register: async (userData: CreateUserModel) => {
      await clientApi.api
        .accountRegisterCreate(userData)
        .then((response: any) => {
          const {
            accessToken,
            expiresAccessToken,
            refreshToken,
            expiresRefreshToken,
          } = response.data.data!;

          return dispatch(
            login({
              logged: true,
              accessToken: accessToken!,
              expiresAccessToken: expiresAccessToken!,
              refreshToken: refreshToken!,
              expiresRefreshToken: expiresRefreshToken!,
            })
          );
        })
        .catch((error: any) => {
          console.error(error);
        });
    },
    login: async (email: string, password: string) => {
      const body = {
        email: email,
        password: encodeURIComponent(password),
      };

      await clientApi.api
        .accountLoginCreate(body)
        .then((response: any) => {
          const {
            accessToken,
            expiresAccessToken,
            refreshToken,
            expiresRefreshToken,
          } = response.data.data!;

          return dispatch(
            login({
              logged: true,
              accessToken: accessToken!,
              expiresAccessToken: expiresAccessToken!,
              refreshToken: refreshToken!,
              expiresRefreshToken: expiresRefreshToken!,
            })
          );
        })
        .catch((error: any) => {
          console.error(error);
        });
    },
    logout: () => {
      dispatch(logout());
      dispatch(cleanData());
    },
  };
}
