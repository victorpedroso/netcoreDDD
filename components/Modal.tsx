import { View, Text, Image } from "react-native";
import Button from "./Button";
import { ReactNativeModal } from "react-native-modal";

interface ModalProps {
    visible: boolean;
    icon?: any;
    title: string;
    description: string;
    label: string;
    onPress: () => void;
}

const Modal = ({ visible, icon, title, description, label, onPress }: ModalProps) => {
    return (
        <ReactNativeModal isVisible={visible}>
            <View className="bg-white px-7 py-9 rounded-2xl">
                {icon && (
                    <Image
                        source={icon}
                        className="w-[110px] h-[110px] mx-auto my-5"
                    />
                )}
                <Text className="text-3xl font-JakartaBold text-center">
                    {title}
                </Text>
                <Text className="text-base text-gray-400 font-Jakarta text-center mt-2">
                    {description}
                </Text>
                <View className="mt-auto">
                    <Button
                        title={label}
                        onPress={onPress}
                        className="mt-5"
                    />
                </View>
            </View>
        </ReactNativeModal>
    );
}

export default Modal;
