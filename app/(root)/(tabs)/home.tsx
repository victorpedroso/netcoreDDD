import { ActivityIndicator, Text, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { useAuthLoginAction } from "@/hooks/useAuthLoginAction";
import { useEffect, useState } from "react";
import { useMeAction } from "@/hooks/useMeAction";
import { useAppSelector } from "@/store/store";
import Container from "@/components/Container";
import { format } from "date-fns";
import Button from "@/components/Button";

const Home = () => {
    const [loading, setLoading] = useState<boolean>(false);
    const me = useAppSelector((state) => state.me);
    const auth = useAppSelector((state) => state.authLogin);
    const meAction = useMeAction();
    const authAction = useAuthLoginAction();

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                await meAction.fetch();
            } finally {
                setLoading(false);
            }
        };
        fetchData();
    }, []);

    if (loading) {
        return (
            <Container classname="flex flex-1 items-center justify-center">
                <ActivityIndicator size="large" />
            </Container>
        );
    }

    const currentDate = format(new Date(), "HH:mm - dd/MM/yyyy");

    return (
        <SafeAreaView className="flex flex-1 bg-white">
            <View className="bg-[#0286FF] rounded-b-3xl p-6 h-[250px] justify-end">
                <Text className="text-white text-4xl font-bold">
                    {`Olá, ${me.name}`}
                </Text>
                <Text className="text-white text-lg">
                    {`${currentDate}`}
                </Text>
            </View>

            <View>
                <Button
                    title="NOVA RESERVA"
                    bgVariant="secondary"
                    textStyle="text-4xl"
                    rounded="lg"
                    className="mt-10 mx-5 h-[100px]"
                    onPress={() => { console.log("Minhas reservas") }}
                />


            </View>

        </SafeAreaView>
    );
};

export default Home;
