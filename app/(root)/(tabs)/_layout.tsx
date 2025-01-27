import { Tabs } from "expo-router";
import { Image, ImageSourcePropType, View } from "react-native";
import { icons } from "@/constants";

const TabIcon = ({ source, focused }: { source: ImageSourcePropType; focused: boolean; }) => (
    <View
        className={`flex flex-row justify-center items-center rounded-full`}
    >
        <View
            className="rounded-full w-12 h-12 items-center justify-center"
        >
            <Image
                source={source}
                tintColor={`${focused ? "#0286FF" : "black"}`}
                resizeMode="contain"
                className="w-7 h-7"
            />
        </View>
    </View>
);

export default function Layout() {
    return (
        <Tabs
            initialRouteName="home"
            screenOptions={{
                tabBarActiveTintColor: "#0286FF",
                tabBarInactiveTintColor: "black",
                tabBarShowLabel: true,
                tabBarStyle: {
                    backgroundColor: "white",
                    overflow: "hidden",
                    height: 78,
                    display: "flex",
                    justifyContent: "center",
                    flexDirection: "row",
                    position: "absolute",
                },
                tabBarLabelStyle: {
                    fontSize: 12,
                    fontWeight: "600",
                    textAlign: "center",
                },
                tabBarItemStyle: {
                    justifyContent: "center",
                },
            }}
        >
            <Tabs.Screen
                name="home"
                options={{
                    title: "Home",
                    headerShown: false,
                    tabBarIcon: ({ focused }) => (
                        <TabIcon source={icons.home} focused={focused} />
                    ),
                }}
            />
            <Tabs.Screen
                name="reservations"
                options={{
                    title: "Reservas",
                    headerShown: false,
                    tabBarIcon: ({ focused }) => (
                        <TabIcon source={icons.list} focused={focused} />
                    ),
                }}
            />
            <Tabs.Screen
                name="chat"
                options={{
                    title: "Chat",
                    headerShown: false,
                    tabBarIcon: ({ focused }) => (
                        <TabIcon source={icons.chat} focused={focused} />
                    ),
                }}
            />
            <Tabs.Screen
                name="profile"
                options={{
                    title: "Perfil",
                    headerShown: false,
                    tabBarIcon: ({ focused }) => (
                        <TabIcon source={icons.profile} focused={focused} />
                    ),
                }}
            />
        </Tabs>
    );
}
