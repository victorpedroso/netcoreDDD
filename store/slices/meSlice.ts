import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { UserModel } from "@/apis/client/ClientApi";

const initialState: UserModel = {
  name: null,
  email: null,
  cpf: null,
  role: null,
  phone: null,
};

const meSlice = createSlice({
  name: "me",
  initialState,
  reducers: {
    setUserData: (state, { payload }: PayloadAction<UserModel>) => {
      state.name = payload.name;
      state.email = payload.email;
      state.cpf = payload.cpf;
      state.role = payload.role;
      state.phone = payload.phone;
    },
    cleanData: (state) => {
      state.name = null;
      state.email = null;
      state.cpf = null;
      state.role = null;
      state.phone = null;
    },
  },
});

export const { setUserData, cleanData } = meSlice.actions;
export default meSlice.reducer;
