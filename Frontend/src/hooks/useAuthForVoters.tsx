import { jwtDecode } from "jwt-decode";
import {useContext} from "react";
import {AuthContextForVoters} from "../context/AuthContextForVoters.tsx";
import {
    IDCardNumberKeyName, IDCardNumberTokenKey, IsNationalMinoritiesKeyName, IsNationalMinoritiesTokenKey,
    NationalMinoritiesEnumName,
    NationalMinoritiesEnumTokenKey, ResidenceCardNumberKeyName, ResidenceCardNumberTokenKey,
    TokenKeyNameForVoter, VotingDistrictName, VotingDistrictTokenKey
} from "../constants/constants.ts";
import api from "../api/api.ts";


const useAuth = () => {
    const { tokenForVoter, setTokenForVoter ,IDCardNumber, setIDCardNumber, ResidenceCardNumber, setResidenceCardNumber, IsNationalMinorities, setIsNationalMinorities, NationalMinoritiesEnum, setNationalMinoritiesEnum, VotingDistrict, setVotingDistrict} = useContext(AuthContextForVoters)


    const isAuthedVoter = !!tokenForVoter;

    const login = async (idcardnumber: string, residencecardnumber: string) => {
        // eslint-disable-next-line no-useless-catch
        try{
            const response = await api.Auth.loginForVoters({idcardnumber: idcardnumber, residencecardnumber: residencecardnumber});

            if (response.data.success && response.data.data) {
                const token = response.data.data.token;


                const decoded: any = jwtDecode(token);
                const IDCardNumber = decoded[IDCardNumberTokenKey];
                const ResidenceCardNumber = decoded[ResidenceCardNumberTokenKey];
                const IsNationalMinorities = decoded[IsNationalMinoritiesTokenKey];
                const NationalMinoritiesEnum = decoded[NationalMinoritiesEnumTokenKey];
                const VotingDistrict = decoded[VotingDistrictTokenKey];

                setTokenForVoter(token);
                localStorage.setItem(TokenKeyNameForVoter, token);

                setIDCardNumber(IDCardNumber);
                localStorage.setItem(IDCardNumberKeyName, IDCardNumber);

                setResidenceCardNumber(ResidenceCardNumber);
                localStorage.setItem(ResidenceCardNumberKeyName, ResidenceCardNumber);

                setIsNationalMinorities(IsNationalMinorities);
                localStorage.setItem(IsNationalMinoritiesKeyName, IsNationalMinorities);

                setNationalMinoritiesEnum(NationalMinoritiesEnum);
                localStorage.setItem(NationalMinoritiesEnumName, NationalMinoritiesEnum);

                setVotingDistrict(VotingDistrict);
                localStorage.setItem(VotingDistrictName, VotingDistrict);

            }else {
                throw new Error(response.data.message || "Ismeretlen hiba");
            }
        }catch (error) {
            throw error;
        }
    }
    const logout=() => {
        localStorage.clear();
        setTokenForVoter(null);
    }

    return{login, logout, isAuthedVoter: isAuthedVoter, tokenForVoter, IDCardNumber, ResidenceCardNumber, IsNationalMinorities,NationalMinoritiesEnum, VotingDistrict}
}

export default useAuth;