import {useContext} from "react";
import { UsersContext} from "../contexts/UsersContext.jsx";

export default function useUsers(){
    const { users } = useContext(UsersContext) ;
    return users;
}

