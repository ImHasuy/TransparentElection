import Landing from "../pages/Landing.tsx";
import VotingStartPage from "@/pages/VotingStartPage.tsx";
import LayerOneVerification from "@/pages/LayerOneVerification.tsx";

export const routes = [
    {
        path: "Landing",
        component: < Landing />,
        isAuthedVoter: false,
        isPrivate: false,
        isPublic: true,
    },
    {
        path: "VotingStartPage",
        component: < VotingStartPage />,
        isAuthedVoter: true,
        isPrivate: false,
        isPublic: false,
    },
    {
        path: "LayerOneVerification",
        component: < LayerOneVerification />,
        isAuthedVoter: false,
        isPrivate: false,
        isPublic: true,
    }

]