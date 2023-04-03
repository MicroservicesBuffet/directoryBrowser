import * as React from "react"
import {
  ChakraProvider,
  Box,
  theme,
  Button,
} from "@chakra-ui/react"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import DisplayFiles from "./DisplayFiles";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
// import { ColorModeSwitcher } from "./ColorModeSwitcher"

export const App = () => {

  const [userName,setUserName]=useState("  please wait");
  const obtainUser=(): Promise<any>=>
    fetch(''+process.env.REACT_APP_URL+'api/usefull/user/authorization', 
    
    {
      method: 'GET',
      credentials: 'include' 
    }
    )
        .then(
          (res) => {
            if(res.status === 200 )
              return res.json() ;
            
              return {name: 'Error'};
          }
      )
  ;
  
    useEffect(() => {
      obtainUser()
      .then(it=> {
        setUserName(it.name);
      });
  
  }, []);
  const queryClient = new QueryClient();
  
return (
  <>
  <ChakraProvider theme={theme}>

  <div>
   <h1> Directory browser and editor   
   <Button colorScheme='teal'>
    <Link color="red"  to="/help/" target="_blank">
    Help
    </Link>
    </Button>
    <span style={{float: 'right'}} >{userName}</span>   
</h1>&nbsp;
    
       
          
  </div>
  <Box>
    
    <QueryClientProvider client={queryClient}>
      <DisplayFiles  />
    </QueryClientProvider>
  </Box>
   
  </ChakraProvider>
  </>
)
  }