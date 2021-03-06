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
const Tick_Rate = 30;

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
            // Add client to cache
            clientCache.add(ws);

            // print client's ip address
            const ipAddr = req.headers['x-forwarded-for'] || req.headers.host;
            console.log(`# A new client [${ws.id}] connects to the server [${ipAddr}]`);

            ws.on('ping', (message) => {
                ws.send();
            });

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
                const result = clientCache.removeById(ws.id);
                console.log(`# A client [${result}] disconnects from the server [${ipAddr}]`);
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
            const ids = clientCache.getIds();

            if (messageQueue.getSize() > 0) {
                const msg = messageQueue.pop();

                // process messages
                for (const id of ids) {
                    const opponent = clientCache.getClient(id);

                    //send message to opponent
                    if (opponent !== msg.getOwner()) {
                        opponent.send(msg.getPayload());
                    }
                }
            }
        }, execTime);
    }
}

module.exports = WsServer;
