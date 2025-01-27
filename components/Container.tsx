import { SafeAreaView } from "react-native";
import { ReactNode } from "react";

const Container = ({ children, classname }: { children: ReactNode, classname?: string }) => (
    <SafeAreaView className={`flex flex-1 bg-white px-4 ${classname}`}>
        {children}
    </SafeAreaView>
);

export default Container;
