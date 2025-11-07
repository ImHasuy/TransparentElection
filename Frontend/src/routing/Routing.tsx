//Ha valaki nincs regisztrálva akkor redirect a loginra
import type {ReactElement} from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import {routes} from "./Routes.tsx";
import useAuth from "../hooks/useAuth.tsx";


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


        //Public oldalak kezelése
        {
            routes.filter(c => !c.isGuestOnly).map(route => (
                <Route
                    key={route.path}
                    path={route.path}
                    element={route.component}
                />
            ))
        }

        {
            routes.filter(route => !route.isPrivate).map(route => (
                <Route
                    key={route.path}
                    path={route.path}
                    element={<AuthenticatedRedirect element={route.component}/>}
                />
            ))
        }




        //Lates a Navbar has to be made to have a BasicLayout
        <Route
            path="app"
            element={<PrivateRoute element={<Navigate to="Landing" />}/>}>
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