import { combineReducers } from "@reduxjs/toolkit";
import authLoginSlice from "./authLoginSlice";
import meSlice from "./meSlice";

const appReducer = combineReducers({
  authLogin: authLoginSlice,
  me: meSlice,
});

const rootReducer = (state: any, action: any) => {
  return appReducer(state, action);
};

export type RootState = ReturnType<typeof rootReducer>;
export default rootReducer;
