import { useAppSelector } from "@/store/store";
import { Redirect } from "expo-router";

const Home = () => {
  const userData = useAppSelector((state) => state.authLogin);

  if (userData.logged) {
    return <Redirect href="/(root)/(tabs)/home" />
  }

  return (
    <Redirect href="/(auth)/welcome" />
  );
};

export default Home;
