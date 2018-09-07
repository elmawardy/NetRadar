<template>
    <div>
        <h1>استغفر الله</h1>
        <h1>الحمد لله</h1>
        <v-app>
            <v-btn color="success">Success</v-btn>
            <v-btn color="error">Error</v-btn>
            <v-btn color="warning">Warning</v-btn>
            <v-btn color="info">Info</v-btn>
            <v-alert :value="true"
                     type="success">
                This is a success alert.
            </v-alert>
            <v-progress-circular :value="20"></v-progress-circular>
        </v-app>
    </div>
</template>