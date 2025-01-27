import Button from "@/components/Button";
import InputField from "@/components/InputField";
import OAuth from "@/components/OAuth";
import { icons, images } from "@/constants";
import { Link } from "expo-router";
import { useState } from "react";
import { Text, ScrollView, View, Image } from "react-native";
import { CreateUserModel } from "@/apis/client/ClientApi";
import { useClientApi } from "@/apis/client";
import Modal from "@/components/Modal";
import { router } from "expo-router";
import { ActivityIndicator } from 'react-native';
import { useAuthLoginAction } from "@/hooks/useAuthLoginAction";

interface SignUpForm extends CreateUserModel {
    confirmPassword: string;
}


const SignUp = () => {
    const authAction = useAuthLoginAction();
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string>("");
    const [form, setForm] = useState<SignUpForm>({
        name: "",
        email: "",
        cpf: "",
        phone: "",
        password: "",
        confirmPassword: "",
    });

    const mapFormToUserModel = (form: SignUpForm): CreateUserModel => {
        const { confirmPassword, ...rest } = form;
        return { ...rest };
    };

    const handleSignUp = async () => {
        setLoading(true);
        if (form.name === "" || form.email === "" || form.cpf === "" || form.phone === "" || form.password === "" || form.confirmPassword === "") {
            setError("Por favor, preencha todos os campos.");
            setLoading(false);
            return;
        }
        if (form.password !== form.confirmPassword) {
            setError("As senhas devem ser iguais.");
            setLoading(false);
            return;
        }

        const filteredData = mapFormToUserModel(form);


        await authAction.register(filteredData)
            .then(() => {
                console.log("Usuário criado com sucesso.");
                //router.replace("/(root)/(tabs)/home");
            })
            .catch((error: any) => {
                setError(error.message || "Erro ao cadastrar usuário.\nPor favor, tente novamente.");
            });
        setLoading(false);
    };

    return (
        <ScrollView className="flex-1 bg-white">
            <View className="flex-1 bg-white">
                <View className="relative w-full h-[250px]">
                    <Image source={images.signUpCar} className="z-0 w-full h-[250px]" />
                    <Text className="text-2xl text-black font-JakartaSemiBold absolute bottom-5 left-5">Crie sua conta</Text>
                </View>
                <View className="p-5">
                    <InputField
                        label="Nome"
                        placeholder="Digite seu nome"
                        icon={icons.person}
                        value={form.name}
                        onChangeText={(value) => setForm({ ...form, name: value })}
                    />
                    <InputField
                        label="E-mail"
                        placeholder="Digite seu e-mail"
                        icon={icons.email}
                        value={form.email}
                        onChangeText={(value) => setForm({ ...form, email: value })}
                    />
                    <InputField
                        label="CPF"
                        placeholder="Digite seu CPF"
                        icon={icons.eyecross}
                        value={form.cpf}
                        onChangeText={(value) => setForm({ ...form, cpf: value })}
                    />
                    <InputField
                        label="Celular"
                        placeholder="Digite seu celular"
                        icon={icons.to}
                        value={form.phone || ""}
                        onChangeText={(value) => setForm({ ...form, phone: value })}
                    />
                    <InputField
                        label="Senha"
                        placeholder="Digite sua senha"
                        icon={icons.lock}
                        secureTextEntry
                        value={form.password}
                        onChangeText={(value) => setForm({ ...form, password: value })}
                    />
                    <InputField
                        label="Confirme sua senha"
                        placeholder="Digite sua senha novamente"
                        icon={icons.lock}
                        secureTextEntry
                        value={form.confirmPassword}
                        onChangeText={(value) => setForm({ ...form, confirmPassword: value })}
                    />

                    <Button
                        title={`${loading ? "Carregando..." : "Cadastrar"}`}
                        onPress={handleSignUp}
                        className={`mt-6 ${loading ? "bg-gray-500" : "bg-primary-500"}`}
                        IconRight={() => loading && (
                            <ActivityIndicator
                                color="#FFFFFF"
                                size="small"
                            />)}
                        disabled={loading}
                    />

                    <OAuth />

                    <Link href="/sign-in" className="text-lg text-center text-general-200 mt-10">
                        <Text>Já tem uma conta? </Text>
                        <Text className="text-primary-500">Entrar</Text>
                    </Link>
                </View>
                <Modal visible={!!error}
                    title="Atenção"
                    description={error}
                    label="Ok"
                    onPress={() => setError("")}
                />
            </View>
        </ScrollView>
    );
};

export default SignUp;