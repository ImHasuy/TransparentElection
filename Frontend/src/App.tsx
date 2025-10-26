import { useState } from 'react'
import './App.css'
import {MantineProvider} from "@mantine/core";
import { BrowserRouter } from "react-router-dom";

function App() {

    const [token, setToken] = useState(localStorage.getItem(TokenKeyName));
    const [nev, setNev] = useState(localStorage.getItem(NameKeyName));
    const [reszleg,setReszleg] = useState(localStorage.getItem(DepartmentKeyName));
    const [role, setRole] = useState(localStorage.getItem(RoleKeyName));
    const [email, setEmail] = useState(localStorage.getItem(EmailKeyName));
    const [ugyintezoiJogosultsagok, setUgyintezoiJogosultsagok] = useState(() => {
        const raw = localStorage.getItem(AdminPrivilegesKeyName);
        return raw ? JSON.parse(raw) : null;
    });


    return <MantineProvider>
        {/*<Notifications/>*/}
        <BrowserRouter>
            <AuthContext.Provider value={{token, setToken,nev, setReszleg, reszleg, setNev, role, setRole,ugyintezoiJogosultsagok, setUgyintezoiJogosultsagok, email, setEmail}}>
                <Routing/>
            </AuthContext.Provider>
        </BrowserRouter>
    </MantineProvider>;


}
export default App