import Landing from "../pages/Landing.tsx";
import VotingStartPage from "@/pages/VotingStartPage.tsx";
import LayerOneVerification from "@/pages/LayerOneVerification.tsx";

export const routes = [
    {
        path: "Landing",
        component: < Landing />,
        isPrivate: false,
        isGuestOnly: false
    },
    {
        path: "VotingStartPage",
        component: < VotingStartPage />,
        isPrivate: false,
        isGuestOnly: false
    },
    {
        path: "LayerOneVerification",
        component: < LayerOneVerification />,
        isPrivate: false,
        isGuestOnly: false
    }

]