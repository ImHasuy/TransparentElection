//Ha valaki nincs regisztrálva akkor redirect a loginra
import type {ReactElement} from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import {routes} from "./Routes.tsx";
import useAuth from "../hooks/useAuth.tsx";
import BasicLayout from "../components/layout/BasicLayout.tsx";

const PrivateRoute = ({element}: {element: ReactElement}) => {
    const {isLoggedIn} = useAuth();
    return isLoggedIn ? element : <Navigate to="Landing"/>;
};

//Ez azért kell, hogy egy bejelentkezett felhasználó ha mondjuk a logint akarja elérni akkor redirectelve legyen
const AuthenticatedRedirect = ({element} : {element: ReactElement}) =>{
    const {isLoggedIn} = useAuth();
    return isLoggedIn ?<Navigate to="/app"/> : element;
};



const Routing = () => {

    //Ez a rész felel azért, ha valaki a rootot akarja elérni akkor eldönti,hova redirectelje
    return<Routes>
        <Route
            path="/"
            element={<AuthenticatedRedirect element={<Navigate to="Landing" />}/>}
        />
        {
            routes.filter(route => !route.isPrivate).map(route => (
                <Route
                    key={route.path}
                    path={route.path}
                    element={<AuthenticatedRedirect element={route.component}/>}
                />
            ))
        }
        <Route
            path="app"
            element={<PrivateRoute element={<BasicLayout/>}/>}>
            <Route
                path=""
                element={<Navigate to="Landing" />}
            />
            {
                routes.filter(route=> route.isPrivate).map(route=> (
                    <Route
                        key={route.path}
                        path={route.path}
                        element={<PrivateRoute element={route.component}/>}
                    />
                ))
            }
        </Route>
    </Routes>

}
export default Routing;