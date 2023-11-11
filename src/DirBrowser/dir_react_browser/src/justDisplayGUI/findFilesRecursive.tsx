import { Input,  Button, Box } from "@chakra-ui/react";
import { FolderToRead } from "../genericFiles/FolderToRead";

import {
  useQuery,
} from '@tanstack/react-query';

import { useState } from "react";
import DisplayFoldersAndFiles from "./displayFoldersAndFiles";
type PropsFindFilesRecursive = {
    folderParent:string
  };
export default function FindFilesRecursive(
    { folderParent }: PropsFindFilesRecursive 
)
{


  const [search, setStartSearch] = useState(false);
  const [searchText, setSearchText] = useState('');
  const searchString = (text: string ): Promise<FolderToRead[]> =>
  fetch(''+process.env.REACT_APP_URL+'api/v1.0/File/Search/'+text+'/'+folderParent,
  {
    method: 'GET',
    credentials: 'include' 
  }).then(
    (res) => res.json() ,
  )
  
  const handleChangeSearch=(event:any) => setSearchText(event.target.value);

  const { isLoading, error, data ,refetch} = useQuery({
    queryKey: ["find"],
    queryFn: ()=>searchString(searchText),
    onError: (err) => {
      window.alert('error when retrieving search');
    },
    enabled: false,
  }); 
  const findData = ()=>{
    setStartSearch(true);
    refetch();
  }  
  let enablFind= (searchText.length > 3 );
    return <>
 <Box > Find files recursive in {folderParent } </Box>

   <Input  value={searchText} onChange={handleChangeSearch} placeholder='please put here the text that you want to find' size='lg' />

   <Button colorScheme='teal'  onClick={findData}  isDisabled={!enablFind}>
    Find {searchText} on folder {folderParent} 
    </Button>
    {search  && <>
      <Box>
      

     {isLoading? 'Loading...': `Found  ${data?.length} files` }
    {error? 'Error when finding files': ''}
    </Box>
    {data?.length && 
      <DisplayFoldersAndFiles  allData={data} folderParent={folderParent} differentFolderParent={true} />
    }


    </>
  }
  
    </>
}