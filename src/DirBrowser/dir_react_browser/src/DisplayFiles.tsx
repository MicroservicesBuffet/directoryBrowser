import { Link } from 'react-router-dom';
import {
    useQuery,
  } from '@tanstack/react-query'
import { useParams } from 'react-router-dom';
import { FolderToRead } from './genericFiles/FolderToRead';
import DisplayFoldersAndFiles from './justDisplayGUI/displayFoldersAndFiles';
import { Box, Breadcrumb, BreadcrumbItem, BreadcrumbLink } from '@chakra-ui/react';
  

  export default function DisplayFiles(){

    const { ...id } = useParams();

    var root='';
    if(id["*"])
      root= id["*"].toString();
    var isRoot = (root.length === 0);
    var arrSplitFolders:string[]=[];
    if(!isRoot)
      arrSplitFolders=root.split('/').filter(it=>it && it.length>0);

    var key= 'root '+root;
    
    

    const fetchFolders = (): Promise<FolderToRead[]> =>
    fetch(''+process.env.REACT_APP_URL+'api/v1.0/File/GetRootFolders').then(
      (res) => res.json() ,
    );

    const fetchFoldersFromFolder = (): Promise<FolderToRead[]> =>
    fetch(''+process.env.REACT_APP_URL+'api/v1.0/File/GetFolderContent/'+root).then(
      (res) => res.json() ,
    )

    const { isLoading, error, data } = useQuery({
        queryKey: [key],
        queryFn: (root.length===0)? fetchFolders: fetchFoldersFromFolder
      })
    
      if (isLoading) return <>'Loading...'</>
    
      if (error) return <>'An error has occurred: ' + JSON.stringify(error)</>
      

    return <>
    { isRoot && <h1>Root folders</h1>}
    { !isRoot && 
    <>  
    <Box h='40px' bg='pink.100'>

    <Breadcrumb separator='/'>
    <BreadcrumbItem>
    <BreadcrumbLink  as={Link} to='/show'>Root Folders</BreadcrumbLink>
  </BreadcrumbItem>
      { arrSplitFolders.map((it, index,arr)=>{
  
  const prevVals = arr.slice(0, index+1);
  const concat = prevVals.reduce((acc, curVal) => {
    return acc +'/'+ curVal;
  }, '/show');
  return <BreadcrumbItem>
    <BreadcrumbLink as={Link} to={concat}>{it}</BreadcrumbLink>
  </BreadcrumbItem>
      }
   
   )}
</Breadcrumb>
</Box>
    </>}
    
      <DisplayFoldersAndFiles folderParent={root} allData={data!} ></DisplayFoldersAndFiles>
    </>
  }