import { useClientApi } from "@/apis/client";
import { setUserData } from "../store/slices/meSlice";
import { useAppDispatch } from "@/store/store";

export function useMeAction() {
  const apiClient = useClientApi();
  const dispatch = useAppDispatch();

  return {
    fetch: async () => {
      await apiClient.api
        .accountMeInfoList()
        .then((res) => {
          const userData = res.data.data;
          if (userData) {
            dispatch(setUserData(userData));
          } else {
            throw new Error("User not found");
          }
        })
        .catch((error) => {
          throw new Error(error.message);
        });
    },
  };
}
