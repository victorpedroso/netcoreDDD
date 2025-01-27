import { TouchableOpacity, Text, View } from "react-native";
import { ButtonProps } from "@/types/type";

const getBgVariantStyle = (variant: ButtonProps["bgVariant"]) => {
    switch (variant) {
        case "secondary":
            return "bg-gray-500";
        case "danger":
            return "bg-red-500";
        case "success":
            return "bg-green-500";
        case "outline":
            return "bg-transparent border-neutral-300 border-[0.5px]";
        default:
            return "bg-[#0286FF]";
    }
};

const getTextVariantStyle = (variant: ButtonProps["textVariant"]) => {
    switch (variant) {
        case "primary":
            return "text-black";
        case "secondary":
            return "text-gray-100";
        case "danger":
            return "text-red-100";
        case "success":
            return "text-green-100";
        default:
            return "text-white";
    }
};

const Button = ({ onPress, title, bgVariant = "primary", textVariant = "default", IconLeft, IconRight, className, disabled = false, textStyle, rounded = "full", ...props }: ButtonProps) => (
    <TouchableOpacity
        onPress={onPress}
        className={`rounded-${rounded} p-3 flex flex-row justify-center items-center shadow-md shadow-neutral-400/70 ${getBgVariantStyle(bgVariant)} ${className}`}
        {...props}
        disabled={disabled}
    >
        {IconLeft && (
            <>
                <IconLeft />
                <View className="ml-4" />
            </>
        )}
        <Text className={`text-lg font-bold ${textStyle} ${getTextVariantStyle(textVariant)}`}>{title}</Text>
        {IconRight && (
            <>
                <View className="mr-4" />
                <IconRight />
            </>)}
    </TouchableOpacity>
);

export default Button;