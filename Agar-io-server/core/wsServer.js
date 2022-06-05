// External modules
const http = require('http');
const ws = require('ws');

// Internal modules
const ExpressServer = require('./expressServer');
const ClientCache = require('./clientCache');
const MessageQueue = require('./messageQueue');
const Message = require('./message');

// Constants
const Base_Exec_Time = 1000;
const Tick_Rate = 20;

// Variables
const clientCache = new ClientCache();
const messageQueue = new MessageQueue();

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
        this.wss = new ws.Server({ server: expressHttp });

        // Handle websocket events
        this.wss.on('connection', (ws, req) => {
            // print client's ip address
            const ipAddr = req.headers['x-forwarded-for'] || req.headers.host;
            console.log(`# A new client connects to the server ${ipAddr}`);

            // Add client to cache
            clientCache.add(ws);

            // Handle event messages
            ws.on('message', (message) => {
                const msg = new Message(ws, message);
                messageQueue.add(msg);
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
        const keys = clientCache.getKeys();

        setInterval(() => {
            const msg = messageQueue.pop();

            if (messageQueue.getSize() > 0) {
                // process messages
                for (const key in keys) {
                    const opponent = clientCache.getClient(key);

                    // send message to opponent
                    if (opponent !== msg.getOwner()) {
                        opponent.send(msg.getPayload(), (err) => {
                            console.log(err);
                        });
                    }
                }
            }
        }, execTime);
    }
}

module.exports = WsServer;
