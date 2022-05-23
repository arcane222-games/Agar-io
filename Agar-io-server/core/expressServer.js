// External modules
require('dotenv').config();
const http = require('http');
const express = require('express');
const session = require('express-session');

/**
 * @class ExpressServer
 * @author Lee Hong Jun (ARC4NE22)
 * @description Express Server for agar.io client
 *              Last Modified: 2022. 05. 23
 */
class ExpressServer {
    /**
     *  Constructor of ExpressServer
     * @param {boolean} secure Http: false, https: true
     */
    constructor(secure = false) {
        this.isRunning = false;

        // Create express instance
        this.app = express();

        // Set port number
        this.app.set('port', process.env.PORT);

        // Set json parser
        this.app.use(express.json());
        this.app.use(express.urlencoded({ extended: false }));

        // Set express-session
        this.app.use(
            session({
                resave: false,
                saveUninitialized: false,
                secret: process.env.COOKIE_SECRET,
                cookie: {
                    httpOnly: !secure,
                    secure,
                },
            })
        );
    }

    /**
     * Run express web server
     * @returns {http.Server} Returns http server
     */
    run() {
        if (!this.isRunning) {
            this.isRunning = true;
            return this.app.listen(process.env.PORT, () => {
                console.log(
                    `# Websocket server running on port ${process.env.PORT}`
                );
            });
        }
    }
}

module.exports = ExpressServer;
