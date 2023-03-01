import * as React from "react"
import {
  ChakraProvider,
  Box,
  VStack,
  theme,
  StackDivider
} from "@chakra-ui/react"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import DisplayFiles from "./DisplayFiles";
// import { ColorModeSwitcher } from "./ColorModeSwitcher"

export const App = () => {


  const queryClient = new QueryClient();

  
return (
  <ChakraProvider theme={theme}>
    <VStack
  divider={<StackDivider borderColor='gray.200' />}
  spacing={4}
  align='stretch'
>
  <Box textAlign="center"  h='40px' bg='white' fontSize="30px">
    Directory browser and editor
  </Box>
  
  <Box>
    
    <QueryClientProvider client={queryClient}>
      <DisplayFiles  />
    </QueryClientProvider>
  </Box>
</VStack>
    {/* <Box textAlign="center" >
      <Grid minH="100vh" p={3}>
        <ColorModeSwitcher justifySelf="flex-end" />
        <VStack spacing={8} >
          <Text>
            Edit <Code fontSize="xl">src/App.tsx</Code> and save to reload.
          </Text>
          <Link
            color="teal.500"
            href="https://chakra-ui.com"
            fontSize="2xl"
            target="_blank"
            rel="noopener noreferrer"
          >
            Learn Chakra
          </Link>
        </VStack>
      </Grid>
    </Box> */}
  </ChakraProvider>
)
  }