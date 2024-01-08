import React, { useState, useEffect } from 'react';
import { Container, Typography, Box, Paper } from '@mui/material';
import axios from 'axios'

const OverviewPage = () => {

    const [maaserInfo, setMaaserInfo] = useState({
        totalIncome: 0,
        totalMaaserGiven: 0,
        maaserObligated: 0,
        remainingMaaserObligation:0
    });

    useEffect(() => {
        const loadMaaserInfo = async() => {
            const { data } = await axios.get('/api/maasertracker/getmaaseroverview');
            setMaaserInfo(data)
        }
        loadMaaserInfo();
    },[])

    return (

        <Container
            maxWidth="md"
            sx={{
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center',
                alignItems: 'center',
                height: '80vh',
                textAlign: 'center'
            }}
        >
            <Paper elevation={3} sx={{ padding: '120px', borderRadius: '15px' }}>
                <Typography variant="h2" gutterBottom>
                    Overview
                </Typography>
                <Box sx={{ marginBottom: '20px' }}>
                    <Typography variant="h5" gutterBottom>
                        Total Income: ${maaserInfo.totalIncome}
                    </Typography>
                    <Typography variant="h5" gutterBottom>
                        Total Maaser Given: ${maaserInfo.totalMaaserGiven}
                    </Typography>
                </Box>
                <Box>
                    <Typography variant="h5" gutterBottom>
                        Maaser Obligated: ${maaserInfo.maaserObligated}
                    </Typography>
                    <Typography variant="h5" gutterBottom>
                        Remaining Maaser obligation: ${maaserInfo.remainingMaaserObligation > 0 ? maaserInfo.remainingMaaserObligation : 0}
                    </Typography>
                </Box>
            </Paper>
        </Container>
    );
}

export default OverviewPage;
