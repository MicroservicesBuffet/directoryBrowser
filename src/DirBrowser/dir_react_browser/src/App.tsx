import * as React from "react"
import {
  ChakraProvider,
  Box,
  theme,
  Button,
  Menu,
  MenuButton,
  MenuItem,
  MenuList,
} from "@chakra-ui/react"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import DisplayFiles from "./DisplayFiles";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { historyData } from "./genericFiles/historyData";
// import { ColorModeSwitcher } from "./ColorModeSwitcher"

export const App = () => {

  const [historyData,setHistoryData]=useState<historyData[]>([]);
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
  

  const lastDataPromise=(): Promise<historyData[]> => 
    fetch(''+process.env.REACT_APP_URL+'short/list/auth/json',{
      method: 'GET',
      credentials: 'include' 
    }).then(it=>it.json())    
    .catch(err=>console.log('err',err));
  ;
const loadHistory=() =>{
  lastDataPromise().then(it=> {
    var sortData=it.sort((a,b)=>a.createdDate>b.createdDate?-1:1);
    setHistoryData(sortData.slice(0,10));
  });
}
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

    <Menu>
  <MenuButton style={{float: 'right'}}  onClick={loadHistory} as={Button} >
  {userName}
  </MenuButton>
  <MenuList>
    {historyData.map(it=>
    <>
      <MenuItem><Link to={it.url}>{it.url}</Link></MenuItem>
    </>
    )}
  </MenuList>
</Menu>
    
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