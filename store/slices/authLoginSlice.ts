import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface AuthLoginState {
  logged: boolean;
  accessToken?: string;
  refreshToken?: string;
  expiresAccessToken?: number;
  expiresRefreshToken?: number;
}

const initialState: AuthLoginState = {
  logged: false,
  accessToken: undefined,
  refreshToken: undefined,
  expiresAccessToken: undefined,
  expiresRefreshToken: undefined,
};

const authLoginSlice = createSlice({
  name: "authLogin",
  initialState,
  reducers: {
    login: (state, { payload }: PayloadAction<AuthLoginState>) => {
      state.logged = true;
      state.accessToken = payload.accessToken;
      state.refreshToken = payload.refreshToken;
      state.expiresAccessToken = payload.expiresAccessToken;
      state.expiresRefreshToken = payload.expiresRefreshToken;
    },
    logout: (state) => {
      state.logged = false;
      state.accessToken = undefined;
      state.refreshToken = undefined;
      state.expiresAccessToken = undefined;
      state.expiresRefreshToken = undefined;
    },
  },
});

export const { login, logout } = authLoginSlice.actions;
export default authLoginSlice.reducer;
