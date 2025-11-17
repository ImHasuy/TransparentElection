import Landing from "../pages/Landing.tsx";
import LayerTwoVerification from "@/pages/LayerTwoVerification.tsx";
import LayerOneVerification from "@/pages/LayerOneVerification.tsx";
import VotingStartPage from "@/pages/VotingStartPage.tsx";
import SingleMemberVotePage from "@/pages/SingleMemberVotePage.tsx";
import SummaryPage from "@/pages/SummaryPage.tsx";

export const routes = [
    {
        path: "Landing",
        component: < Landing />,
        isAuthedVoter: false,
        isPrivate: false,
        isPublic: true,
    },
    {
        path: "LayerTwoVerification",
        component: < LayerTwoVerification />,
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
    },
    {
        path: "VotingStartPage",
        component: < VotingStartPage />,
        isAuthedVoter: true,
        isPrivate: false,
        isPublic: false,
    },
    {
        path: "SingleMemberVotePage",
        component: < SingleMemberVotePage />,
        isAuthedVoter: true,
        isPrivate: false,
        isPublic: false,
    },
    {
        path: "SummaryPage",
        component: < SummaryPage />,
        isAuthedVoter: true,
        isPrivate: false,
        isPublic: false,
    }





]