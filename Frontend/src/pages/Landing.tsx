import {BackgroundImage, Box, Center, Container, Image, LoadingOverlay, Paper, Text} from "@mantine/core";
import {useState} from "react";


function Landing(){

    const [isLoading, setIsLoading] = useState(true);


    return(
        <BackgroundImage src={`/orszaghaz.jpg`} style={{ backgroundImage: `url(/orszaghaz.jpg)`,
            backgroundSize: "cover",
            position: 'sticky',
            backgroundPosition: "center",
            minHeight: "100dvh",
            width: "100vw",
            height: "100dvh",

        }}>
            <Box
                style={{
                    position: "absolute",
                    inset: 0,
                    backdropFilter: "blur(2px)",
                    backgroundColor: "rgba(0,0,0,0.4)", // sötétebb árnyalat a kontraszthoz
                    zIndex: 0,
                }}
            />

            <Container pos='relative' fluid p={0} style={{zIndex:1,textAlign:"center"}}>
                <LoadingOverlay visible={isLoading} overlayProps={{ blur: 2 }}/>
                <Paper p="xl"
                       radius="lg">
                    <Center>
                        <Text>dfsdfsdzd</Text>
                    </Center>
                </Paper>
            </Container>
        </BackgroundImage>
    )
}

export default Landing;