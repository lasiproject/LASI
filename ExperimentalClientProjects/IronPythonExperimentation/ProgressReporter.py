class ProgressReporter (object):
    """description of class"""

    def __init__(self):
        self.progress = 0.0
        self.message = "idling..."
 
    def Report(self, event_args):
        self.progress += event_args.PercentWorkRepresented
        self.message = event_args.Message
        print "Increment:", event_args.PercentWorkRepresented
        print "Message:", event_args.Message

