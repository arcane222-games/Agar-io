// External modules
const http = require('http');
const WebSocket = require('ws');

// Internal modules
const ExpressServer = require('./expressServer');
const ClientCache = require('./clientCache');

// Constants
const Base_Exec_Time = 1000;
const Tick_Rate = 0.5;

const clientCache = new ClientCache();

/**
 * @class WsServer
 * @author Lee Hong Jun (ARC4NE22)
 * @description Websocket Server for agar.io client
 *              Last Modified: 2022. 05. 23
 */
class WsServer {
    /**
     * Constructor of WsServer
     * @param {http.Server} expressHttp
     */
    constructor(expressHttp) {
        this.isRunning = false;

        // Create Websocket Server (attach to express http server)
        this.wss = new WebSocket.Server({ server: expressHttp });

        // Handle websocket events
        this.wss.on('connection', (ws, req) => {
            // print client's ip address
            const ipAddr = req.headers['x-forwarded-for'] || req.headers.host;
            console.log(`# A new client connects to the server ${ipAddr}`);

            // Add client to cache
            clientCache.add(ws);

            // Handle event messages
            ws.on('message', (message) => {
                console.log(message);
                ws.send(message);
            });

            // Print out error info
            ws.on('error', (error) => {
                console.error(err);
            });

            // Remove client from cache
            ws.on('close', () => {
                console.log(`# A client disconnects from the server ${ipAddr}`);
                clientCache.removeByValue(ws);
            });
        });
    }

    /**
     * Run http server based on tick rate
     */
    run() {
        this.isRunning = true;
        const execTime = Base_Exec_Time / Tick_Rate;
        setInterval(() => {
            console.log(Object.keys(clientCache.cache).length);

            for (let key in clientCache.cache) {
                const ws = clientCache.getClient(key);
                if (ws.readyState === ws.OPEN) {
                }
            }
        }, execTime);
    }
}

module.exports = WsServer;
