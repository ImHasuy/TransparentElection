//Ha valaki nincs regisztrálva akkor redirect a loginra
import type {ReactElement} from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import {routes} from "./Routes.tsx";
import useAuth from "../hooks/useAuth.tsx";
import useAuthForVoters from "@/hooks/useAuthForVoters.tsx";


const PrivateRoute = ({element}: {element: ReactElement}) => {
    const {isLoggedIn} = useAuth();
    return isLoggedIn ? element : <Navigate to="/Landing"/>;
};
const AuthenticatedRedirect = ({ element }: { element: ReactElement }) => {
    const { isLoggedIn } = useAuth();
    return isLoggedIn ? <Navigate to="/app" /> : element;
};
const AuthenticatedVoterRedirect = ({element} : {element: ReactElement}) =>{
    const {isAuthedVoter} = useAuthForVoters();
    return isAuthedVoter ? element : <Navigate to="/Landing"/>;
};



const GetElementForRoute = (route: any) : ReactElement => {
    if(route.isAuthedVoter) {
        return <AuthenticatedVoterRedirect element={route.component}/>
    }
    if(route.isPrivate){
        return <PrivateRoute element={route.component}/>
    }
    if(route.isPublic){
        return route.component;
    }
    return route.component;
}


const Routing = () => {
    return<Routes>
        <Route
            path="/"
            element={<AuthenticatedRedirect element={<Navigate to="Landing" />}/>}
        />

        {routes.map(route => (
            <Route
                key={route.path}
                path={route.path}
                element={GetElementForRoute(route)}

            />
        ))}

    </Routes>

}
export default Routing;