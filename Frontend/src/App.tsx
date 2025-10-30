import { useState } from 'react'
import {MantineProvider} from "@mantine/core";
import { BrowserRouter } from "react-router-dom";
import {EmailKeyName, NameKeyName, RoleKeyName, TokenKeyName} from "./constants/constants.ts";
import Routing from "./routing/Routing.tsx";
import { AuthContext } from "./context/AuthContext.tsx";

function App() {

    const [token, setToken] = useState(localStorage.getItem(TokenKeyName));
    const [nev, setNev] = useState(localStorage.getItem(NameKeyName));
    const [role, setRole] = useState(localStorage.getItem(RoleKeyName));
    const [email, setEmail] = useState(localStorage.getItem(EmailKeyName));


    return <MantineProvider>
        {/*<Notifications/>*/}
        <BrowserRouter>
            <AuthContext.Provider value={{token, setToken,nev, setNev, role, setRole, email, setEmail}}>
                <Routing/>
            </AuthContext.Provider>
        </BrowserRouter>
    </MantineProvider>;


}
export default App