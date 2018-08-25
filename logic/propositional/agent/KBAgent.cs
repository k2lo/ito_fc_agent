using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using aima.core.agent;
using aima.core.agent.impl;
using aima.core.logic.fol.kb;
using aima.core.logic.propositional.kb;
using aima.core.logic.propositional.parsing.ast;

namespace aima.core.logic.propositional.agent
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): Figure 7.1, page
     * 236.<br>
     * <br>
     * 
     * <pre>
     * function KB-AGENT(percept) returns an action
     *   persistent: KB, a knowledge base
     *               t, a counter, initially 0, indicating time
     *           
     *   TELL(KB, MAKE-PERCEPT-SENTENCE(percept, t))
     *   action &lt;- ASK(KB, MAKE-ACTION-QUERY(t))
     *   TELL(KB, MAKE-ACTION-SENTENCE(action, t))
     *   t &lt;- t + 1
     *   return action
     * 
     * </pre>
     * 
     * Figure 7.1 A generic knowledge-based agent. Given a percept, the agent adds
     * the percept to its knowledge base, asks the knowledge base for the best
     * action, and tells the knowledge base that it has in fact taken that action.
     * 
     * @author Ciaran O'Reilly
     */
    public abstract class KBAgent : AbstractAgent
    {
	// persistent: KB, a knowledge base
	protected FOLKnowledgeBase KB;
	// t, a counter, initially 0, indicating time
	private int t = 0;

		public KBAgent(FOLKnowledgeBase KB)
	{
	    this.KB = KB;
	}

	// function KB-AGENT(percept) returns an action
        
	public override core.agent.Action execute(Percept p)
	{
	    // TELL(KB, MAKE-PERCEPT-SENTENCE(percept, t))
	    KB.tell(makePerceptSentence(p, t));
		// action &lt;- ASK(KB, MAKE-ACTION-QUERY(t))
		core.agent.Action action = ask(KB, makeActionQuery(t));

	    // TELL(KB, MAKE-ACTION-SENTENCE(action, t))
	    KB.tell(makeActionSentence(action, t));
	    // t &lt;- t + 1
	    t = t + 1;
	    // return action
	    return action;
	}

	/**
	 * MAKE-PERCEPT-SENTENCE constructs a sentence asserting that the agent
	 * perceived the given percent at the given time.
	 * 
	 * @param percept
	 *            the given percept
	 * @param t
	 *            the given time
	 * @return a sentence asserting that the agent perceived the given percept
	 *         at the given time.
	 */
	// MAKE-PERCEPT-SENTENCE(percept, t)
	public abstract String makePerceptSentence(Percept percept, int t);

	/**
	 * MAKE-ACTION-QUERY constructs a sentence that asks what action should be
	 * done at the current time.
	 * 
	 * @param t
	 *            the current time.
	 * @return a sentence that asks what action should be done at the current
	 *         time.
	 */
	// MAKE-ACTION-QUERY(t)
	public abstract Sentence makeActionQuery(int t);

	/**
	 * MAKE-ACTION-SENTENCE constructs a sentence asserting that the chosen action was executed.
	 * @param action
	 *        the chose action.
	 * @param t
	 *        the time at which the action was executed.
	 * @return a sentence asserting that the chosen action was executed.
	 */
	// MAKE-ACTION-SENTENCE(action, t)
	public abstract String makeActionSentence(core.agent.Action action, int t);

	/**
	 * A wrapper around the KB's ask() method which translates the action (in the form of
	 * a sentence) determined by the KB into an allowed 'Action' object from the current
	 * environment in which the KB-AGENT resides.
	 * 
	 * @param KB
	 *        the KB to ask.
	 * @param actionQuery
	 *        an action query.
	 * @return the Action to be performed in response to the given query.
	 */
	// ASK(KB, MAKE-ACTION-QUERY(t))
		public abstract core.agent.Action ask(FOLKnowledgeBase KB, Sentence actionQuery);
    }
}