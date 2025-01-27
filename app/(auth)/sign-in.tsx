import Button from "@/components/Button";
import InputField from "@/components/InputField";
import OAuth from "@/components/OAuth";
import { icons, images } from "@/constants";
import { Link, router } from "expo-router";
import { useState } from "react";
import { Text, ScrollView, View, Image, ActivityIndicator } from "react-native";
import Modal from "@/components/Modal";
import { useAuthLoginAction } from "@/hooks/useAuthLoginAction";
import { useAppSelector } from "@/store/store";
import { useMeAction } from "@/hooks/useMeAction";

const SignIn = () => {
    const { login } = useAuthLoginAction();
    const [form, setForm] = useState({
        email: "",
        password: "",
    });
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string>("");
    const meAction = useMeAction();

    const handleSignIn = async () => {
        setLoading(true);

        if (form.email === "" || form.password === "") {
            setError("Por favor, preencha todos os campos.");
            setLoading(false);
            return;
        }

        await login(form.email, form.password)
            .then(() => {
                //await meAction.fetch();
                router.replace("/(root)/(tabs)/home");
            })
            .catch((error) => {
                setError(error.message || "Erro ao fazer login.\nPor favor, tente novamente.");
            });

        setLoading(false);
    };

    return (
        <ScrollView className="flex-1 bg-white">
            <View className="flex-1 bg-white">
                <View className="relative w-full h-[250px]">
                    <Image source={images.signUpCar} className="z-0 w-full h-[250px]" />
                    <Text className="text-2xl text-black font-JakartaSemiBold absolute bottom-5 left-5">Bem vindo(a)</Text>
                </View>
                <View className="p-5">
                    <InputField
                        label="E-mail"
                        placeholder="Digite seu e-mail"
                        icon={icons.email}
                        value={form.email}
                        onChangeText={(value) => setForm({ ...form, email: value })}
                    />
                    <InputField
                        label="Senha"
                        placeholder="Digite sua senha"
                        icon={icons.lock}
                        secureTextEntry
                        value={form.password}
                        onChangeText={(value) => setForm({ ...form, password: value })}
                    />
                    <Button
                        title={loading ? "Carregando..." : "Entrar"}
                        onPress={handleSignIn}
                        className="mt-6"
                        IconRight={() => loading && (
                            <ActivityIndicator
                                color="#FFFFFF"
                                size="small"
                            />)}
                        disabled={loading}
                    />

                    <OAuth />

                    <Link href="/sign-up" className="text-lg text-center text-general-200 mt-10">
                        <Text>Não tem uma conta? </Text>
                        <Text className="text-primary-500">Cadastrar</Text>
                    </Link>
                </View>
                <Modal visible={!!error}
                    title="Erro"
                    description={error}
                    label="Ok"
                    onPress={() => setError("")} />
            </View>
        </ScrollView>
    );
};

export default SignIn;