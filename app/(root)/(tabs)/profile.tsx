import InputField from "@/components/InputField";
import { useState } from "react";
import { ActivityIndicator, Image, ScrollView, Text, View } from "react-native";
import { useSelector } from "react-redux";
import Button from "@/components/Button";
import Container from "@/components/Container";
import { useAuthLoginAction } from "@/hooks/useAuthLoginAction";
import { router } from "expo-router";
import Constants from "expo-constants";

const Profile = () => {
    const me = useSelector((state: any) => state.me);
    const [loading, setLoading] = useState<boolean>(false);
    const [editable, setEditable] = useState<boolean>(false);
    const { logout } = useAuthLoginAction();
    const version = Constants?.expoConfig?.version;

    const handleUpdate = () => {
        setLoading(true);
        setLoading(false);
        setEditable(false);
    };

    const allowEdit = () => {
        setEditable(true);
    }

    const handleLogout = () => {
        logout();
        router.replace("/(auth)/sign-in");
    }
    return (
        <Container>
            <ScrollView
                className="flex-1 bg-white"
                contentContainerStyle={{ paddingBottom: 120 }}
            >
                <Text className="text-2xl font-JakartaBold my-5">Meu Perfil</Text>

                <View className="flex items-center justify-center my-5">
                    <Image
                        source={{
                            uri: "",
                        }}
                        style={{ width: 110, height: 110, borderRadius: 110 / 2 }}
                        className=" rounded-full h-[110px] w-[110px] border-[3px] border-white shadow-sm shadow-neutral-300"
                    />
                </View>

                <View className="flex flex-col items-start justify-center bg-white rounded-lg shadow-sm shadow-neutral-300 px-5 py-3">
                    <View className="flex flex-col items-start justify-start w-full">
                        <InputField
                            label="Nome"
                            placeholder="Nome"
                            value={me.name}
                            containerStyle="w-full"
                            inputStyle="p-3.5"
                            editable={editable}
                        />

                        <InputField
                            label="E-mail"
                            value={me.email}
                            placeholder="E-mail"
                            containerStyle="w-full"
                            inputStyle="p-3.5"
                            editable={editable}
                        />

                        <InputField
                            label="CPF"
                            value={me.cpf}
                            placeholder="CPF"
                            containerStyle="w-full"
                            inputStyle="p-3.5"
                            editable={editable}
                        />

                        <Button
                            title={editable ? "Salvar" : "Editar"}
                            onPress={editable ? handleUpdate : allowEdit}
                            className="w-full mt-5"
                            IconRight={() => loading && (
                                <ActivityIndicator
                                    color="#FFFFFF"
                                    size="small"
                                />)}
                        />

                        <Button
                            title="Sair"
                            onPress={handleLogout}
                            className="w-full mt-5"
                            bgVariant="danger"
                        />
                        <View className="top-10">
                            <Text className="text-sm text-neutral-400">Versão {version}</Text>
                        </View>

                    </View>
                </View>
            </ScrollView>
        </Container>
    );
};

export default Profile;