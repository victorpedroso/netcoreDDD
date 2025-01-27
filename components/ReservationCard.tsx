import React from "react";
import { TouchableOpacity, View, Text } from "react-native"

type ReservationCardProps = {
    label: string;
    Icon?: React.ComponentType<any>;
    description?: string;
    value: string;
    onPress: () => void;
    classname?: string
}

const ReservationCard = ({ label, Icon, description, value, onPress, classname }: ReservationCardProps) => {
    return (
        <TouchableOpacity
            onPress={onPress}
            className={`rounded-2xl p-3 flex flex-row justify-center items-center shadow-md shadow-neutral-400/70 ${classname}`}
        >
            {Icon && (
                <>
                    <Icon />
                    <View className="ml-4" />
                </>
            )}
            <Text>{label}</Text>

        </TouchableOpacity>
    )
}

export default ReservationCard
